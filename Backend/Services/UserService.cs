using Backend.Models.DTOs.User;
using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Wrappers;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;

namespace Backend.Services;

public class UserService
{
  private readonly IUserRepository _userRepo;
  private readonly VocabularyListService _vocabListService;

  public UserService(IUserRepository userRepo, VocabularyListService vocabListService)
  {
    _userRepo = userRepo;
    _vocabListService = vocabListService;
  }
  public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
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

        return new UserDto
        {
          Id = user.Id,
          Username = user.Username,
          PasswordHash = user.PasswordHash,
          Email = user.Email,
          VocabularyLists = [.. lists
            .Select(vl => new VocabularyListSummaryDto
            {
                Id = vl.Id,
                Title = vl.Title,
                VocabularyCount = vl.Vocabularies.Count
            })]
        };
      });

      var userDtos = await Task.WhenAll(tasks);

      return new ApiResponse<IEnumerable<UserDto>>
      {
        Success = true,
        Message = "Userdtos retrieved successfully",
        Data = userDtos
      };
    }
    catch (Exception ex)
    {
      return new ApiResponse<IEnumerable<UserDto>>
      {
        Success = false,
        Message = "Userdtos retrieved unsuccessfully " + ex.Message,
        Data = []
      };
    }
  }

  public async Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(Guid userId)
  {
    try
    {

      var user = await _userRepo.GetByIdAsync(userId)
          ?? throw new InvalidOperationException("User not found");
      var listsResponse = await _vocabListService.GetListsByUserAsync(userId);
      var lists = listsResponse.Success && listsResponse.Data != null ? listsResponse.Data : [];

      var userProfile = new UserProfileDto
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        VocabularyLists = [.. lists]
      };

      return new ApiResponse<UserProfileDto>
      {
        Success = true,
        Message = "User profile retrieved successfully",
        Data = userProfile
      };
    }
    catch (Exception ex)
    {
      return new ApiResponse<UserProfileDto>
      {
        Success = false,
        Message = "User profile retrieved unsuccessfully " + ex.Message,
        Data = null
      };
    }
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


  public async Task<ApiResponse<bool>> DeleteUserAsync(Guid userId)
  {
    try
    {
      var user = await _userRepo.GetByIdAsync(userId);
      if (user == null)
      {
        return new ApiResponse<bool>
        {
          Success = false,
          Message = "User not found",
          Data = false
        };
      }

      await _userRepo.DeleteAsync(user);

      return new ApiResponse<bool>
      {
        Success = true,
        Message = "User deleted successfully",
        Data = true
      };
    }
    catch (Exception ex)
    {
      return new ApiResponse<bool>
      {
        Success = false,
        Message = $"Failed to delete user: {ex.Message}",
        Data = false
      };
    }
  }
}
