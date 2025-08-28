using Backend.DTOs;
using Backend.Repositories;

namespace Backend.Services;

public class UserService
{
  private readonly IUserRepository _userRepo;

  public UserService(IUserRepository userRepo)
  {
    _userRepo = userRepo;
  }

  public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
  {
    var users = await _userRepo.GetAllUsersAsync();

    return users.Select(u => new UserDto
    {
      Id = u.Id,
      Username = u.Username,
      PasswordHash = u.PasswordHash,
      Email = u.Email,
      VocabularyLists = [.. u.VocabularyLists.Select(vl => new VocabularyListSummaryDto
      {
        Id = vl.Id,
        Title = vl.Title,
        VocabularyCount = vl.Vocabularies.Count
      })]
    });
  }

  public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
  {
    var user = await _userRepo.GetByIdAsync(userId);
    if (user == null)
        return null;

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
      if (user == null)
        return false;

      await _userRepo.DeleteAsync(user);
      return true;
    }
    catch
    {
      return false;
    }
  }
}
