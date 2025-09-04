using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Entities;
using Backend.Models.Wrappers;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;
using Memora.Models.DTOs.Vocabulary;

public class VocabularyService
{
  private readonly IVocabularyRepository _vocRepo;
  private readonly IVocabularyListRepository _vocListRepo;
  public VocabularyService(IVocabularyRepository vocRepo, IVocabularyListRepository vocListRepo)
  {
    _vocRepo = vocRepo;
    _vocListRepo = vocListRepo;
  }

  public async Task<ApiResponse<VocabularyWrapper>> GetAllVocabulariesAsync()
  {
    try
    {
      var vocs = await _vocRepo.GetAllVocabularies();
      if (vocs == null || vocs.Count == 0)
      {
        return new ApiResponse<VocabularyWrapper>
        {
          Success = true,
          Message = "No vocabularies found",
          Data = null
        };
      }

      var lists = await _vocListRepo.GetAllLists();
      var mappedLists = lists.Select(MapToDto).ToList();

      var orphans = vocs
        .Where(v => !lists.Any(l => l.Id == v.VocabularyListId))
        .ToList();

      var result = new VocabularyWrapper
      {
        VocabularyLists = mappedLists,
        Orphans = orphans
      };

      return new ApiResponse<VocabularyWrapper>
      {
        Success = true,
        Message = "Vocabularies retrieved successfully",
        Data = result
      };
    }
    catch (Exception ex)
    {
      return new ApiResponse<VocabularyWrapper>
      {
        Success = false,
        Message = "Vocabularies retrieved unsuccessfully: " + ex.Message,
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
      CreatedAt = list.CreatedAt,
      Vocabularies = [.. list.Vocabularies.Select(v => new VocabularyDto
          {
              Id = v.Id,
              Word = v.Word,
              Translation = v.Translation,
          })]
    };
  }
}

public class VocabularyWrapper
{
  public List<VocabularyListDto>? VocabularyLists { get; set; }
  public List<Vocabulary>? Orphans { get; set; }
}
