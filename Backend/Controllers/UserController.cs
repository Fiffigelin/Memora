using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.DTOs.User;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
  private readonly UserService _userService = userService;

  [HttpGet("{userId:guid}")]
  public async Task<ActionResult<UserProfileDto>> GetUserProfile(Guid userId)
  {
    try
    {
      var profile = await _userService.GetUserProfileAsync(userId);
      if (profile == null)
      { 
        return NotFound(new { message = "User not found" });
      }

      return Ok(profile);
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }

  [HttpGet("profiles")]
  public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
  {
    var profiles = await _userService.GetAllUserProfilesAsync();
    return Ok(profiles);
  }

  [HttpGet("all")]
  public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
  {
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
  }

  [HttpDelete("{userId:guid}")]
  public async Task<ActionResult> DeleteUser(Guid userId)
  {
    try
    {
      await _userService.DeleteUserAsync(userId);
      return NoContent();
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }
}
