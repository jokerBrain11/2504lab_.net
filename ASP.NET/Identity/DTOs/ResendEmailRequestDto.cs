using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class ResendEmailRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set;}
}