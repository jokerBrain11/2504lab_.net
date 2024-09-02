using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.DTOs;
using Identity.Services;

[Route("api/[controller]")] // 設置路由
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 註冊 API 端點
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(request);
        string message = result.Succeeded ? "註冊成功，請至信箱確認帳號" : result.Errors.FirstOrDefault()?.Description;
        return Ok(new { message });
    }

    /// <summary>
    /// 確認電子郵件 API 端點
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.ConfirmEmailAsync(request);
        string message = result.Succeeded ? "電子郵件確認成功。" : "確認電子郵件時出錯。";
        return Ok(new { message });
    }


    /// <summary>
    /// 檢查電子郵件是否已確認 API 端點
    /// </summary>
    /// <returns></returns>
    [HttpPost("check-confirmed-email")]
    public async Task<IActionResult> CheckConfirmedEmail([FromBody] CheckConfirmedEmailRequestDto request)
    {
        var result = await _userService.CheckConfirmedEmail(request);
        string message = result.Succeeded ? "電子郵件已確認" : "電子郵件未確認";
        return Ok(new { message });
    }

    /// <summary>
    /// 重新發送電子郵件 API 端點
    /// </summary>
    /// <returns></returns>
    [HttpPost("resend-email")]
    public async Task<IActionResult> ResendEmail([FromBody] ResendEmailRequestDto request)
    {
        var result = await _userService.ResendEmail(request);
        string message = result.Succeeded ? "電子郵件已重新發送" : result.Errors.FirstOrDefault()?.Description;
        return Ok(new { message });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.LoginUserAsync(request);
        if (result.Succeeded)
        {
            HttpContext.Response.Cookies.Append("token", result.Token, result.cookieOptions);
            return Ok(new { message = result.Message });
        }
        return BadRequest(new { message = result.Message });

    }

    /// <summary>
    /// 登出 API 端點
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            return BadRequest("用戶未登入");
        }
        await _userService.LogoutUserAsync();
        return Ok("登出成功");
    }

    /// <summary>
    /// 獲取用戶資料 API 端點
    /// </summary>
    /// <returns></returns>
    [Authorize] // 需要驗證
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return BadRequest("用戶未登入");
        }
        var user = await _userService.GetProfileAsync();
        return Ok(user);
    }

    /// <summary>
    /// 創建角色 API 端點
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")] // 需要 Admin 角色才能訪問
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody]CreateRoleAsyncRequestDto request)
    {
        var result = await _userService.CreateRoleAsync(request);
        if (result.Succeeded)
        {
            return Ok("角色創建成功");
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errors });
    }

    /// <summary>
    /// 分配角色 API 端點
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Policy = "RequireAdminManager")] // 需要 Admin 角色才能訪問
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody]AssignRoleRequestDto request)
    {
        var result = await _userService.AssignRole(request);
        if (result.Succeeded)
        {
            return Ok("角色分配成功");
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errors });
    }
}
