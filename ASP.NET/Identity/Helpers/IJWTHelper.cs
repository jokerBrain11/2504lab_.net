using Identity.Models;

namespace Identity.Helpers
{
    public interface IJWTHelper
    {
        /// <summary>
        /// 生成 Cookie 選項
        /// </summary>
        /// <param name="Expires">Cookie 過期時間（分鐘）</param>
        /// <returns>CookieOptions 實例</returns>
        /// <remarks>Expires 默認值為 20 分鐘</remarks>
        CookieOptions GenerateCookieOptions(int Expires = 20);
        /// <summary>
        ///  產生JWT Token
        /// </summary>
        /// <param name="jWTCliam">Token 資訊聲明內容物件</param>
        /// <returns>回應內容物件，內容屬性jwt放置Token字串</returns>
        RunStatus GetJWT(JWTCliam jWTCliam, List<string> roles);
    }
}