using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")] // 設置路由
public class UserController : ControllerBase
{
    private readonly IUserHelper _userHelper;
    private readonly ILogger<UserController> _logger;
    private readonly IJWTHelper _jwtHelper;

    // 建構函數，注入 IUserHelper 和 ILogger 實例
    public UserController(ILogger<UserController> logger, IUserHelper userHelper, IJWTHelper jwtHelper)
    {
        _logger = logger;
        _userHelper = userHelper;
        _jwtHelper = jwtHelper;
    }

    // 註冊 API 端點
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userHelper.RegisterUserAsync(request);
        if (result.Succeeded)
        {
            var message = _userHelper.RequiresConfirmedEmail
                ? "註冊成功，請檢查您的電子郵件以確認您的帳戶。"
                : "註冊成功";
            return Ok(new { message });
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errors });
    }

    // 確認電子郵件 API 端點
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
        {
            return BadRequest("必須提供用戶 ID 和確認碼。");
        }

        var result = await _userHelper.ConfirmEmailAsync(userId, code);
        if (result.Succeeded)
        {
            // 這裡不直接分配角色，應根據實際需求決定是否需要
            return Ok("電子郵件確認成功。");
        }

        return BadRequest("確認電子郵件時出錯。");
    }

    // 檢查是否需要確認電子郵件 API 端點
    [HttpPost("check-confirmed-email")]
    public async Task<IActionResult> CheckConfirmedEmail(string email)
    {
        if (await _userHelper.CheckConfirmedEmail(email))
        {
            return Ok("電子郵件已經確認。");
        }
        return Ok("電子郵件未確認。");
    }

    // 登入 API 端點
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userHelper.LoginUserAsync(request);
        if (result.Succeeded)
        {
            // JWT Token 生成
            var user = await _userHelper.GetProfileAsync();
            var clims = new JWTCliam
            {
                sub = user.Id,
                name = user.UserName,
                email = user.Email
            };
            var roles = await _userHelper.GetRolesAsync(user);
            var jwtResult = _jwtHelper.GetJWT(clims, roles);
            if (jwtResult.isSuccess)
            {
                HttpContext.Response.Cookies.Append("authToken", jwtResult.jwt, _jwtHelper.GenerateCookieOptions());
                return Ok(new { message = "登入成功", jwt = jwtResult.jwt });
            }
        }

        return BadRequest("登入嘗試無效。");
    }

    // 登出 API 端點
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _userHelper.LogoutUserAsync();
        return Ok("登出成功");
    }

    // 獲取用戶資料 API 端點
    [Authorize] // 需要授權才能訪問
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _userHelper.GetProfileAsync();
        if (user == null)
        {
            return NotFound(); // 未找到用戶
        }
        return Ok(user);
    }

    // 創建角色 API 端點
    [Authorize(Roles = "admin")] // 需要 Admin 角色才能訪問
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (User.Identities.Any(i => i.Name == "admin"))
        {
            return BadRequest("無權限創建角色");
        }
        var result = await _userHelper.CreateRoleAsync(roleName);
        if (result.Succeeded)
        {
            return Ok("角色創建成功");
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errors });
    }

    // 分配角色 API 端點
    [Authorize(Policy = "RequireAdminManager")] // 需要 Admin 角色才能訪問
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(string userName, string roleName)
    {
        var result = await _userHelper.AssignRole(userName, roleName);
        if (result.Succeeded)
        {
            return Ok("角色分配成功");
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errors });
    }
}
