using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Identity.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserHelper> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTHelper _jwtHelper;

        public bool RequiresConfirmedEmail => throw new NotImplementedException();

        // 建構函數，注入 UserManager、SignInManager、IEmailSender 和 ILogger 實例
        public UserHelper(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<UserHelper> logger,
            IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleManager,
            IJWTHelper jwtHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _jwtHelper = jwtHelper;
        }

        // 註冊用戶方法
        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                ProfilePictureUrl = request.ProfilePictureUrl
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("用戶使用密碼創建了新帳戶。");

                // 生成電子郵件確認令牌
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var requestScheme = _httpContextAccessor.HttpContext.Request.Scheme;
                var requestHost = _httpContextAccessor.HttpContext.Request.Host.Value;

                var callbackUrl = $"{requestScheme}://{requestHost}/api/user/confirm-email?userId={user.Id}&code={code}";

                try
                {
                    // 發送包含確認鏈接的電子郵件
                    await _emailSender.SendEmailAsync(request.Email, "確認您的電子郵件",
                        $"請通過 <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>點擊這裡</a> 來確認您的帳戶。");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "發送電子郵件確認鏈接時發生錯誤。");
                    return IdentityResult.Failed(new IdentityError { Description = "無法發送電子郵件確認。" });
                }
                
                return result;
            }

            return result;
        }

        // 登入用戶方法
        public async Task<SignInResult> LoginUserAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("用戶登入成功。");
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    _logger.LogInformation($"用戶角色：{string.Join(",", roles)}");
                }
                else
                {
                    _logger.LogWarning("用戶無角色分配。");
                }
            }
            return result;
        }

        // 登出用戶方法
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // 確認電子郵件方法
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }

            if (user.EmailConfirmed)
            {
                return IdentityResult.Failed(new IdentityError { Description = "電子郵件已經確認。" });
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        // 檢查是否需要確認電子郵件方法
        public async Task<bool> CheckConfirmedEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            return user.EmailConfirmed;
        }

        // 獲取用戶資料方法
        public async Task<ApplicationUser> GetProfileAsync()
        {
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);
            return user;
        }

        // 創建角色方法
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "角色已存在。" });
            }

            var role = new IdentityRole(roleName);
            return await _roleManager.CreateAsync(role);
        }

        // 獲取角色方法
        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        // 分配角色方法
        public async Task<IdentityResult> AssignRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "角色未找到。" });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
