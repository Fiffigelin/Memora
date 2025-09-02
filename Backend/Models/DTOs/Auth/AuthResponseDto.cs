using Backend.Models.DTOs.User;

namespace Backend.Models.DTOs.Auth;

public class AuthResponseDto
{
  public string Token { get; set; } = string.Empty;
  public UserProfileDto? User { get; set; }
}
