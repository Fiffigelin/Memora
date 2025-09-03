using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Entities;
using Backend.Models.Wrappers;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;
using Memora.Models.DTOs.Vocabulary;

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

	public async Task<ApiResponse<VocabularyListDto>> UpdateVocabularyListsAsync(Guid userId, UpdateVocabularyListDto dto)
	{
		try
		{
			var list = await _listRepo.GetListById(dto.Id);
			if (list == null || list.UserId != userId)
			{
				return new ApiResponse<VocabularyListDto>
				{
					Success = false,
					Message = "List not found or not owned by user",
					Data = null
				};
			}

			list.Title = dto.Title;

			var existingVocabularies = list.Vocabularies.ToList();
			var dtoIds = dto.Vocabularies.Where(v => v.Id.HasValue)
																	 .Select(v => v.Id!.Value)
																	 .ToHashSet();

			var toRemove = existingVocabularies.Where(v => !dtoIds.Contains(v.Id)).ToList();
			foreach (var r in toRemove)
			{
				existingVocabularies.Remove(r);
			}

			var newVocabs = new List<Vocabulary>();
			foreach (var v in dto.Vocabularies)
			{
				if (v.Id.HasValue)
				{
					var existing = existingVocabularies.FirstOrDefault(x => x.Id == v.Id.Value);
					if (existing != null)
					{
						existing.Word = v.Word;
						existing.Translation = v.Translation;
					}
				}
				else
				{
					newVocabs.Add(new Vocabulary
					{
						Id = Guid.NewGuid(),
						Word = v.Word,
						Translation = v.Translation,
						VocabularyListId = list.Id
					});
				}
			}

			await _listRepo.UpdateListAsync(list, toRemove, newVocabs);

			return new ApiResponse<VocabularyListDto>
			{
				Success = true,
				Message = "List updated successfully",
				Data = MapToDto(list)
			};
		}
		catch (Exception ex)
		{
			return new ApiResponse<VocabularyListDto>
			{
				Success = false,
				Message = "Failed to update list: " + ex.Message,
				Data = null
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
