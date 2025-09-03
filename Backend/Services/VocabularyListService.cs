using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Entities;
using Backend.Models.Wrappers;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;

public class VocabularyListService
{
	private readonly IVocabularyListRepository _listRepo;
	private readonly IUserRepository _userRepo;
	public VocabularyListService(IVocabularyListRepository listRepo, IUserRepository userRepo)
	{
		_listRepo = listRepo;
		_userRepo = userRepo;
	}
	public async Task<ApiResponse<VocabularyListDto>> CreateListAsync(Guid userId, CreateVocabularyListDto dto)
	{
		try
		{
			var user = await _userRepo.GetByIdAsync(userId)
					?? throw new InvalidOperationException("User not found");
			var list = MapToEntity(dto, user);

			await _listRepo.AddListAsync(list);

			return new ApiResponse<VocabularyListDto>
			{
				Success = true,
				Message = "List created successfully",
				Data = MapToDto(list)
			};
		}
		catch (Exception ex)
		{
			return new ApiResponse<VocabularyListDto>
			{
				Success = false,
				Message = "List created unsuccessfully: " + ex.Message,
				Data = null
			};
		}
	}

	public async Task<ApiResponse<IEnumerable<VocabularyListDto>>> GetListsByUserAsync(Guid userId)
	{
		try
		{
			var lists = await _listRepo.GetListsByUser(userId);
			var result = lists.Select(MapToDto).ToList();

			return new ApiResponse<IEnumerable<VocabularyListDto>>
			{
				Success = true,
				Message = "Lists retrieved successfully",
				Data = result
			};
		}
		catch (Exception ex)
		{
			return new ApiResponse<IEnumerable<VocabularyListDto>>
			{
				Success = false,
				Message = "Lists retrieved unsuccessfully: " + ex.Message,
				Data = null
			};
		}
	}

	public async Task<ApiResponse<IEnumerable<VocabularyListDto>>> GetAllListsAsync()
	{
		try
		{
			var lists = await _listRepo.GetAllLists();
			var result = lists.Select(MapToDto).ToList();

			return new ApiResponse<IEnumerable<VocabularyListDto>>
			{
				Success = true,
				Message = "Lists retrieved successfully",
				Data = result
			};
		}
		catch (Exception ex)
		{
			return new ApiResponse<IEnumerable<VocabularyListDto>>
			{
				Success = false,
				Message = "Lists retrieved unsuccessfully: " + ex.Message,
				Data = []
			};
		}
	}

	private static VocabularyListDto MapToDto(VocabularyList list)
	{
		return new VocabularyListDto
		{
			Id = list.Id,
			Title = list.Title,
			Language = list.Language,
			Vocabularies = [.. list.Vocabularies.Select(v => new VocabularyDto
					{
							Id = v.Id,
							Word = v.Word,
							Translation = v.Translation
					})]
		};
	}
	private static VocabularyList MapToEntity(CreateVocabularyListDto dto, User user)
	{
		return new VocabularyList
		{
			Id = Guid.NewGuid(),
			Title = dto.Title,
			Language = dto.Language,
			UserId = user.Id,
			User = user,
			Vocabularies = [.. dto.Vocabularies.Select(v => new Vocabulary
						{
							Id = Guid.NewGuid(),
							Word = v.Word,
							Translation = v.Translation
						})]
		};
	}
}
