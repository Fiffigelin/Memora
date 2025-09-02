using Backend.Models.Entities;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Vocabularies;

public class VocabularyListRepository : IVocabularyListRepository
{
  private readonly MemoraDbContext _context;

  public VocabularyListRepository(MemoraDbContext context)
  {
    _context = context;
  }

  public async Task<VocabularyList> AddListAsync(VocabularyList list)
  {
    _context.VocabularyLists.Add(list);
    await _context.SaveChangesAsync();
    return list;
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
