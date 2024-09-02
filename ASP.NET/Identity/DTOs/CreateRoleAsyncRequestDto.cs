using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class CreateRoleAsyncRequestDto
{
    [Required]
    public string RoleName { get; set; }
}