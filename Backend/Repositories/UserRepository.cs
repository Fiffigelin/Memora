
using Backend.Data;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
  private readonly MemoraDbContext _context;

  public UserRepository(MemoraDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(User user)
  {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> ExistsByUsernameAsync(string username)
  {
    return await _context.Users.AnyAsync(u => u.Username == username);
  }

  public async Task<bool> ExistsByEmailAsync(string email)
  {
    return await _context.Users.AnyAsync(u => u.Email == email);
  }

  public async Task<User> GetByIdAsync(Guid userId)
  {
    var user = await _context.Users
        .Include(u => u.VocabularyLists)
            .ThenInclude(vl => vl.Vocabularies)
        .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

    return user;
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    var user = await _context.Users
        .Include(u => u.VocabularyLists)
            .ThenInclude(vl => vl.Vocabularies)
        .FirstOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException($"User with Email {email} not found.");

    return user;
  }
  public async Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return await _context.Users
        .Include(u => u.VocabularyLists)
            .ThenInclude(vl => vl.Vocabularies)
        .ToListAsync();
  }

  public async Task<User> DeleteAsync(User user)
  {
    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
    return user;
  }

  // public User? GetByUsername(string username)
  // {
  //   return _context.Users.FirstOrDefault(u => u.Username == username);
  // }

  // public void Update(User user)
  // {
  //   _context.Users.Update(user);
  //   _context.SaveChanges();
  // }
}
