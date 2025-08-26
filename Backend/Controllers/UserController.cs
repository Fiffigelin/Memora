using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models.Auth;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
  private readonly UserService _userService = userService;

  [HttpPost("register")]
  public async Task<ActionResult<bool>> Register([FromBody] RegisterUserDto dto)
  {
    try
    {
      return await _userService.Register(dto);

    }
    catch (ArgumentException ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpGet("{userId:guid}")]
  public async Task<ActionResult<UserProfileDto>> GetUserProfile(Guid userId)
  {
    try
    {
      var profile = await _userService.GetUserProfileAsync(userId);
      return Ok(profile);
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }

  [HttpGet("{UserProfiles}")]
  public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
  {
    var profiles = await _userService.GetAllUserProfilesAsync();
    return Ok(profiles);
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
