using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Helpers
{
    public interface IUserHelper
    {
        // 判斷是否需要確認電子郵件
        bool RequiresConfirmedEmail { get; }

        // 註冊用戶方法
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto request);

        // 登入用戶方法
        Task<SignInResult> LoginUserAsync(LoginRequestDto request);

        // 登出用戶方法
        Task LogoutUserAsync();

        // 確認電子郵件方法
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);

        // 檢查是否需要確認電子郵件方法
        Task<bool> CheckConfirmedEmail(string email);

        // 獲取用戶資料方法
        Task<ApplicationUser> GetProfileAsync();

        // 創建角色方法
        Task<IdentityResult> CreateRoleAsync(string roleName);

        // 獲取角色方法
        Task<List<string>> GetRolesAsync(ApplicationUser user);

        // 分配角色方法
        Task<IdentityResult> AssignRole(string userName, string roleName);
    }
}
