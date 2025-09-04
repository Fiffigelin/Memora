using Backend.Models.Entities;

namespace Backend.Repositories.Vocabularies;

public interface IVocabularyRepository
{
  Task<List<Vocabulary>> GetAllVocabularies();
}