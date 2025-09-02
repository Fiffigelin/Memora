

using Backend.Models.DTOs.Vocabulary;

namespace Backend.Repositories.Vocabularies;

public interface IVocabularyRepository
{
  Task AddVocubalarityList(VocabularyListDto voca);
  Task<IEnumerable<VocabularyListDto>> GetAllVocabulariesByUserAsync();
  Task<VocabularyListDto> DeleteVocubalarityList(Guid userId, Guid vocaId);
}