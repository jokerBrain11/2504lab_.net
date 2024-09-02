using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class LoginRequestDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}