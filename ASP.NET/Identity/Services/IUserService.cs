using Microsoft.AspNetCore.Identity;

using Identity.DTOs;

namespace Identity.Services
{
    public interface IUserService
    {
        // 判斷是否需要確認電子郵件
        bool RequiresConfirmedEmail { get; set;}

        /// <summary>
        /// 註冊用戶方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto request);
        /// <summary>
        /// 確認電子郵件方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailRequestDto request);
        
        /// <summary>
        /// 檢查是否需要確認電子郵件
        /// </summary>
        /// <returns></returns>
        Task<IdentityResult> CheckConfirmedEmail(CheckConfirmedEmailRequestDto request);

        /// <summary>
        /// 重新發送電子郵件
        /// </summary>
        /// <returns></returns>
        Task<IdentityResult> ResendEmail(ResendEmailRequestDto request);

        /// <summary>
        /// 登入用戶方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<LoginUserResponseDto> LoginUserAsync(LoginRequestDto request);

        /// <summary>
        /// 登出用戶方法
        /// </summary>
        /// <returns></returns>
        Task LogoutUserAsync();

        /// <summary>
        /// 獲取用戶資料方法
        /// </summary>
        /// <returns></returns>
        Task<GetProfileResponseDto> GetProfileAsync();

        /// <summary>
        /// 新增角色方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateRoleAsync(CreateRoleAsyncRequestDto request);

        /// <summary>
        /// 分配角色方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IdentityResult> AssignRole(AssignRoleRequestDto request);
    }
}
