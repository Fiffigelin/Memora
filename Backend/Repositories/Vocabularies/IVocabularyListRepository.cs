using Backend.Models.Entities;

namespace Backend.Repositories.Vocabularies;

public interface IVocabularyListRepository
{
  Task<VocabularyList> AddListAsync(VocabularyList list);
  Task<List<VocabularyList>> GetListsByUser(Guid userId);
  Task<List<VocabularyList>> GetAllLists();
  Task<VocabularyList?> GetListById(Guid listId);
  Task<VocabularyList> UpdateListAsync(VocabularyList list, IEnumerable<Vocabulary> vocToRemove, IEnumerable<Vocabulary> vocToAdd);
}