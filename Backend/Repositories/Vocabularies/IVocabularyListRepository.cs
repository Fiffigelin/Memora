

using Backend.DTOs.Vocabulary;

namespace Backend.Repositories.Vocabularies;

public interface IVocabularyListRepository
{
  Task AddVocabularyListAsync(VocabularyListDto voca);
  Task<List<VocabularyList>> GetListsByUser(Guid userId);
  Task<List<VocabularyList>> GetAllLists();
}