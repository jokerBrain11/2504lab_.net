using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class ConfirmEmailRequestDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string Code { get; set; }
}