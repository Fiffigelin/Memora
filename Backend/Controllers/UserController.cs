using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models.DTOs.User;
using Backend.Models.Wrappers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
  private readonly UserService _userService = userService;

  [HttpGet]
  public async Task<ActionResult<ApiResponse<UserProfileDto>>> GetUserProfile()
  {
    try
    {
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
      if (userIdClaim == null)
        return Unauthorized();

      var userId = Guid.Parse(userIdClaim.Value);

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

  [AllowAnonymous]
  [ApiExplorerSettings(IgnoreApi = true)]
  [HttpGet("profiles")]
  public async Task<ActionResult<ApiResponse<IEnumerable<UserProfileDto>>>> GetAllUserProfiles()
  {
    var profiles = await _userService.GetAllUserProfilesAsync();
    return Ok(profiles);
  }

  [AllowAnonymous]
  [ApiExplorerSettings(IgnoreApi = true)]
  [HttpGet("all")]
  public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers()
  {
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
  }

  [HttpDelete]
  public async Task<ActionResult<ApiResponse<bool>>> DeleteUser()
  {
    try
    {
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
      if (userIdClaim == null)
        return Unauthorized();

      var userId = Guid.Parse(userIdClaim.Value);

      await _userService.DeleteUserAsync(userId);
      return NoContent();
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }
}
