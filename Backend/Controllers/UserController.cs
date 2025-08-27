using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.DTOs;
using Backend.Entities;

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

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
  {
    var authResponse = await _userService.LoginAsync(loginDto);

    if (authResponse == null)
      return Unauthorized("Invalid email or password.");

    return Ok(authResponse);
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
