using Backend.Models.DTOs.Auth;
using Backend.Models.Entities;
using Backend.Repositories;
using Backend.Validations;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services.Auth;

public interface IAuthService
{
  Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
  Task<AuthResultDto> RegisterAsync(RegisterUserDto dto);
}

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepo;
  private readonly IPasswordHasher<User> _hasher;
  private readonly ITokenService _tokenService;
  private readonly UserService _userService;

  public AuthService(IUserRepository userRepo, IPasswordHasher<User> hasher, ITokenService tokenService, UserService userService)
  {
    _userRepo = userRepo;
    _hasher = hasher;
    _tokenService = tokenService;
    _userService = userService;
  }

  public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
  {
    var user = await _userRepo.GetByEmailAsync(dto.Email);
    if (user == null)
      return null;

    var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
    if (result == PasswordVerificationResult.Failed)
      return null;

    var token = _tokenService.GenerateToken(user);

    var userProfile = await _userService.GetUserProfileAsync(user.Id);

    return new AuthResponseDto
    {
      Token = token,
      User = userProfile.Data
    };
  }

  public async Task<AuthResultDto> RegisterAsync(RegisterUserDto dto)
  {
    User? user = null;

    try
    {

      await ValidateUser(dto);

      user = new User(dto.Username, dto.Email);
      user.UpdatePassword(_hasher.HashPassword(user, dto.Password));

      await _userRepo.AddAsync(user);

      return new AuthResultDto
      {
        Success = true,
        Message = "User registered successfully."
      };
    }
    catch (ArgumentException ex)
    {
      return new AuthResultDto
      {
        Success = false,
        Message = ex.Message
      };
    }
    catch (Exception)
    {
      if (user != null)
      {
        await _userRepo.DeleteAsync(user);
      }

      return new AuthResultDto
      {
        Success = false,
        Message = "An unexpected error occurred. Please try again later."
      };
    }
  }

  private async Task ValidateUser(RegisterUserDto dto)
  {
    if (!InputValidators.IsValidEmail(dto.Email))
      throw new ArgumentException("Invalid email format.");

    if (!InputValidators.IsValidUsername(dto.Username))
      throw new ArgumentException("Invalid username format.");

    if (!InputValidators.IsValidPassword(dto.Password))
      throw new ArgumentException(
          "Password must be at least 8 characters, include at least one uppercase letter, " +
          "one lowercase letter, one number, and one special character.");

    if (await _userRepo.ExistsByUsernameAsync(dto.Username))
      throw new ArgumentException("Username already exists");

    if (await _userRepo.ExistsByEmailAsync(dto.Email))
      throw new ArgumentException("Email already exists");
  }
}
