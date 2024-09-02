namespace Identity.DTOs;

public class LoginUserResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }
    public CookieOptions? cookieOptions { get; set; }
}