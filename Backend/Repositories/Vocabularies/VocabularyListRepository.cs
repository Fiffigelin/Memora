
using Backend.Data;
using Backend.DTOs.Vocabulary;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Vocabularies;

public class VocabularyListRepository : IVocabularyListRepository
{
  private readonly MemoraDbContext _context;

  public VocabularyListRepository(MemoraDbContext context)
  {
    _context = context;
  }

  public Task AddVocabularyListAsync(VocabularyListDto voca)
  {
    throw new NotImplementedException();
  }

  public async Task<List<VocabularyList>> GetListsByUser(Guid userId)
  {
      return await _context.VocabularyLists
          .Where(vl => vl.UserId == userId)
          .Include(vl => vl.Vocabularies)
          .ToListAsync();
  }
  public async Task<List<VocabularyList>> GetAllLists()
  {
      return await _context.VocabularyLists
          .Include(vl => vl.Vocabularies)
          .ToListAsync();
  }
}
