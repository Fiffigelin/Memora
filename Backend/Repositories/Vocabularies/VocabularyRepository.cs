using Backend.Models.Entities;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Vocabularies;

public class VocabularyRepository : IVocabularyRepository
{
  private readonly MemoraDbContext _context;

  public VocabularyRepository(MemoraDbContext context)
  {
    _context = context;
  }

  public async Task<List<Vocabulary>> GetAllVocabularies()
  {
    return await _context.Vocabularies.ToListAsync();
  }
}
