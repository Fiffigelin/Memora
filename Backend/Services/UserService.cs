using Backend.Models.DTOs.User;
using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Wrappers;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;

namespace Backend.Services;

public class UserService
{
  private readonly IUserRepository _userRepo;
  private readonly IVocabularyListRepository _vocabListRepo;
  private readonly VocabularyListService _vocabListService;

  public UserService(IUserRepository userRepo, IVocabularyListRepository vocabListRepo, VocabularyListService vocabListService)
  {
    _userRepo = userRepo;
    _vocabListRepo = vocabListRepo;
    _vocabListService = vocabListService;
  }

  public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
  {
    var users = await _userRepo.GetAllUsersAsync();
    var lists = await _vocabListRepo.GetAllLists();

    return users.Select(u => new UserDto
    {
      Id = u.Id,
      Username = u.Username,
      PasswordHash = u.PasswordHash,
      Email = u.Email,
      VocabularyLists = [.. lists
            .Where(vl => vl.UserId == u.Id)
            .Select(vl => new VocabularyListSummaryDto
            {
                Id = vl.Id,
                Title = vl.Title,
                VocabularyCount = vl.Vocabularies.Count
            })]
    });
  }

  public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
  {
    var user = await _userRepo.GetByIdAsync(userId)
        ?? throw new InvalidOperationException("User not found");
    var lists = await _vocabListRepo.GetListsByUser(userId);

    return new UserProfileDto
    {
      Id = user.Id,
      Username = user.Username,
      Email = user.Email,
      VocabularyLists = [.. lists.Select(vl => new VocabularyListDto
            {
              Id = vl.Id,
              Title = vl.Title,
              Language = vl.Language,
              Vocabularies = [.. vl.Vocabularies
                    .Select(v => new VocabularyDto
                    {
                      Id = v.Id,
                      Word = v.Word,
                      Translation = v.Translation
                    })]
            })]
    };
  }

  public async Task<ApiResponse<IEnumerable<UserProfileDto>>> GetAllUserProfilesAsync()
  {
    try
    {
      var users = await _userRepo.GetAllUsersAsync();

      var tasks = users.Select(async user =>
      {
        var listsResponse = await _vocabListService.GetListsByUserAsync(user.Id);
        var lists = listsResponse.Success && listsResponse.Data != null
      ? listsResponse.Data
      : [];

        return new UserProfileDto
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email,
          VocabularyLists = [.. lists]
        };
      });

      var userProfiles = await Task.WhenAll(tasks);

      return new ApiResponse<IEnumerable<UserProfileDto>>
      {
        Success = true,
        Message = "All users profiles retrieved successfully",
        Data = userProfiles
      };
    }
    catch (Exception ex)
    {
      return new ApiResponse<IEnumerable<UserProfileDto>>
      {
        Success = false,
        Message = "Failed to users profiles: " + ex.Message,
        Data = null
      };
    }
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
