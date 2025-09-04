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
        .OrderByDescending(l => l.CreatedAt)
        .ToListAsync();
  }

  public async Task<VocabularyList?> GetListById(Guid listId)
  {
    return await _context.VocabularyLists
        .Include(vl => vl.Vocabularies)
        .FirstOrDefaultAsync(vl => vl.Id == listId);
  }

  public async Task<VocabularyList> UpdateListAsync(VocabularyList list, IEnumerable<Vocabulary> vocToRemove, IEnumerable<Vocabulary> vocToAdd)
  {
    if (vocToRemove?.Any() == true)
    {
      foreach (var v in vocToRemove)
        _context.Vocabularies.Attach(v);

      _context.Vocabularies.RemoveRange(vocToRemove);
    }

    if (vocToAdd?.Any() == true)
    {
      foreach (var v in vocToAdd)
        _context.Vocabularies.Add(v);

      _context.VocabularyLists.Attach(list);
    }

    await _context.SaveChangesAsync();
    return list;
  }

  public async Task DeleteList(VocabularyList list)
  {
    _context.VocabularyLists.Remove(list);
    var result = await _context.SaveChangesAsync();
  }
}
