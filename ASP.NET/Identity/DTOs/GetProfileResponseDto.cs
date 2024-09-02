using Identity.Models;

namespace Identity.DTOs;

public class GetProfileResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public ApplicationUser? Data { get; set; }
}