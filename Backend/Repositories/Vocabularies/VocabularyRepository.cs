
using Backend.Data;
using Backend.Models.DTOs.Vocabulary;

namespace Backend.Repositories.Vocabularies;

public class VocabularyRepository : IVocabularyRepository
{
  private readonly MemoraDbContext _context;

  public VocabularyRepository(MemoraDbContext context)
  {
    _context = context;
  }

  public Task AddVocubalarityList(VocabularyListDto voca)
  {
    // create the VocabularyList entity and add the vocabularies to it
    throw new NotImplementedException();
  }

  public Task<IEnumerable<VocabularyListDto>> GetAllVocabulariesByUserAsync()
  {
    // fetch all vocabulary lists for a specific user
    throw new NotImplementedException();
  }

  public Task<VocabularyListDto> DeleteVocubalarityList(Guid userId, Guid vocaId)
  {
    // delete a specific vocabulary list for a user
    throw new NotImplementedException();
  }
}
