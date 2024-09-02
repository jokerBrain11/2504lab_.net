using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class AssignRoleRequestDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string RoleName { get; set; }
}