using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class CheckConfirmedEmailRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set;}
}