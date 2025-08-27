using Backend.DTOs;
using Backend.Entities;
using Backend.Repositories;
using Backend.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services;

public interface IUserService
{
  Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto);
}

public class UserService : IUserService
{
  private readonly IUserRepository _userRepo;
  private readonly PasswordHasher<User> _hasher;
  private readonly IConfiguration _configuration;

  public UserService(IUserRepository userRepo, IConfiguration configuration)
  {
    _userRepo = userRepo;
    _hasher = new PasswordHasher<User>();
    _configuration = configuration;
  }

  public async Task<bool> Register(RegisterUserDto dto)
  {
    User? user = null;

    try
    {

      await ValidateUser(dto);

      user = new User(dto.Username, dto.Email);
      user.UpdatePassword(_hasher.HashPassword(user, dto.Password));

      await _userRepo.AddAsync(user);

      return true;
    }
    catch
    {
      if (user != null)
      {
        await _userRepo.DeleteAsync(user);
      }
      return false;
    }
  }

  public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto)
  {
    var user = await _userRepo.GetByEmailAsync(loginDto.Email);

    if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
      return null;

    var token = GenerateJwtToken(user);

    return new AuthResponseDto
    {
      Token = token,
      User = new UserProfileDto
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        VocabularyLists = user.VocabularyLists
                .Select(vl => new VocabularyListSummaryDto
                {
                  Id = vl.Id,
                  Title = vl.Title,
                  VocabularyCount = vl.Vocabularies.Count
                }).ToList()
      }
    };
  }

  public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
  {
    var users = await _userRepo.GetAllUsersAsync();

    return users.Select(u => new UserDto
    {
      Id = u.Id,
      Username = u.Username,
      PasswordHash = u.PasswordHash.
      Email = u.Email,
      VocabularyLists = u.VocabularyLists.Select(vl => new VocabularyListSummaryDto
      {
        Id = vl.Id,
        Title = vl.Title,
        VocabularyCount = vl.Vocabularies.Count
      }).ToList()
    });
  }

  public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
  {
    var user = await _userRepo.GetByIdAsync(userId);

    return new UserProfileDto
    {
      Id = user.Id,
      Username = user.Username,
      Email = user.Email,
      VocabularyLists = [.. user.VocabularyLists
            .Select(vl => new VocabularyListSummaryDto
            {
              Id = vl.Id,
              Title = vl.Title,
              VocabularyCount = vl.Vocabularies.Count
            })]
    };
  }

  public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync()
  {
    var users = await _userRepo.GetAllUsersAsync();

    return [.. users.Select(u => new UserProfileDto
    {
      Id = u.Id,
      Username = u.Username,
      Email = u.Email,
      VocabularyLists = [.. u.VocabularyLists
            .Select(vl => new VocabularyListSummaryDto
            {
              Id = vl.Id,
              Title = vl.Title,
              VocabularyCount = vl.Vocabularies.Count
            })]
    })];
  }


  public async Task<bool> DeleteUserAsync(Guid userId)
  {
    try
    {
      var user = await _userRepo.GetByIdAsync(userId);

      await _userRepo.DeleteAsync(user);
      return true;
    }
    catch
    {
      return false;
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

  // public bool Login(LoginUserDto dto)
  // {
  //   var user = _repo.GetByUsername(dto.Username);
  //   if (user == null) return false;

  //   var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
  //   return result == PasswordVerificationResult.Success;
  // }

  // public User Edit(User user, EditUserDto dto)
  // {
  //   if (!string.IsNullOrWhiteSpace(dto.Username))
  //   {
  //     if (_repo.ExistsByUsername(dto.Username))
  //       throw new ArgumentException("Username already exists");
  //     user.UpdateUsername(dto.Username);
  //   }

  //   if (!string.IsNullOrWhiteSpace(dto.Email))
  //     user.UpdateEmail(dto.Email);

  //   if (!string.IsNullOrWhiteSpace(dto.Password))
  //     user.UpdatePassword(_hasher.HashPassword(user, dto.Password));

  //   _repo.Update(user);
  //   return user;
  // }
}
