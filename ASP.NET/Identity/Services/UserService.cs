using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using Identity.Models;
using Identity.DTOs;
using Identity.Helpers;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTHelper _jwtHelper;

        public bool RequiresConfirmedEmail { get; set; }

        // 建構函數，注入 UserManager、SignInManager、IEmailSender 和 ILogger 實例
        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<UserService> logger,
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

        /// <summary>
        /// 註冊用戶方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

            // 檢查用戶是否已經存在
            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶已經註冊。" });
            }

            // 創建新用戶
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // 生成電子郵件確認令牌
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                string? requestScheme = _httpContextAccessor.HttpContext?.Request.Scheme;
                string? requestHost = _httpContextAccessor.HttpContext?.Request.Host.Value;
                string? callbackUrl = $"{requestScheme}://{requestHost}/api/user/confirm-email?userId={user.Id}&code={code}";

                try
                {
                    // 發送包含確認鏈接的電子郵件
                    await _emailSender.SendEmailAsync(request.Email, "確認您的電子郵件",
                        $"請通過 <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>點擊這裡</a> 來確認您的帳戶。");
                    RequiresConfirmedEmail = true;
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

        /// <summary>
        /// 確認電子郵件方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }

            if (user.EmailConfirmed)
            {
                return IdentityResult.Failed(new IdentityError { Description = "電子郵件已經確認。" });
            }

            string code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var confirmResult = await _userManager.ConfirmEmailAsync(user, code);
            // 當用戶確認電子郵件成功時，將用戶添加到角色中
            if (confirmResult.Succeeded)
            {
                // 檢查角色是否存在，如果不存在則創建角色
                var findUserRole = await _roleManager.FindByNameAsync("user");
                var findAdminRole = await _roleManager.FindByNameAsync("admin");
                if (findUserRole == null || findAdminRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }
                // 若用戶名包含 "admin" 則添加到 admin 角色中
                if (user.UserName.Contains("admin"))
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                await _userManager.AddToRoleAsync(user, "user");
            }
            return confirmResult;
        }

        /// <summary>
        /// 檢查是否需要確認電子郵件方法
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CheckConfirmedEmail(CheckConfirmedEmailRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }
            if (user.EmailConfirmed)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = "電子郵件未確認。" });
        }

        /// <summary>
        /// 重新發送電子郵件方法
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ResendEmail(ResendEmailRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }

            if (user.EmailConfirmed)
            {
                return IdentityResult.Failed(new IdentityError { Description = "電子郵件已經確認。" });
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string? requestScheme = _httpContextAccessor.HttpContext?.Request.Scheme;
            string? requestHost = _httpContextAccessor.HttpContext?.Request.Host.Value;
            string? callbackUrl = $"{requestScheme}://{requestHost}/api/user/confirm-email?userId={user.Id}&code={code}";

            try
            {
                await _emailSender.SendEmailAsync(request.Email, "確認您的電子郵件",
                    $"請通過 <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>點擊這裡</a> 來確認您的帳戶。");
                RequiresConfirmedEmail = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發送電子郵件確認鏈接時發生錯誤。");
                return IdentityResult.Failed(new IdentityError { Description = "無法發送電子郵件確認。" });
            }
            return IdentityResult.Success;
        }

        /// <summary>
        /// 登入用戶方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginUserResponseDto> LoginUserAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new LoginUserResponseDto { Succeeded = false, Message = "用戶未找到。" };
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var rolesAsync = await _userManager.GetRolesAsync(user);
                var roles = rolesAsync.ToList();
                // JWT Token 生成
                var clims = new JWTCliam
                {
                    sub = user.Id,
                    name = user.UserName,
                    email = user.Email
                };
                var jwtResult = _jwtHelper.GetJWT(clims, roles);
                if (jwtResult.isSuccess)
                {
                    return new LoginUserResponseDto 
                    {
                        Succeeded = true, 
                        Message = "登入成功", 
                        Token = jwtResult.jwt, 
                        cookieOptions = _jwtHelper.GenerateCookieOptions()
                    };
                }
            }
            return new LoginUserResponseDto { Succeeded = false, Message = "登入嘗試無效。" };
        }

        /// <summary>
        /// 登出用戶方法
        /// </summary>
        /// <returns></returns>
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        /// <summary>
        /// 獲取用戶資料方法
        /// </summary>
        /// <returns></returns>
        public async Task<GetProfileResponseDto> GetProfileAsync()
        {
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);
            if (user == null)
            {
                return new GetProfileResponseDto { Succeeded = false, Message = "用戶未找到。" };
            }
            return new GetProfileResponseDto { Succeeded = true, Data = user };
        }

        /// <summary>
        /// 獲取角色方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        /// <summary>
        /// 新增角色方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRoleAsync(CreateRoleAsyncRequestDto request)
        {
            if (await _roleManager.RoleExistsAsync(request.RoleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "角色已存在。" });
            }

            var role = new IdentityRole(request.RoleName);
            return await _roleManager.CreateAsync(role);
        }

        /// <summary>
        /// 分配角色方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AssignRole(AssignRoleRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "用戶未找到。" });
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "角色未找到。" });
            }

            var loginUser = await _userManager.GetUserAsync(_signInManager.Context.User);
            var loginUserRoles = await _userManager.GetRolesAsync(loginUser);
            if (loginUserRoles.Any(x => x.StartsWith("manager") && x != "admin") && role.Name == "admin")
            {
                return IdentityResult.Failed(new IdentityError { Description = "無法分配高於自身權限之角色。" });
            }

            return await _userManager.AddToRoleAsync(user, role.Name);
        }
    }
}
