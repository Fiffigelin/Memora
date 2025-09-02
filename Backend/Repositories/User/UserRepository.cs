
using Backend.Data;
using Backend.Models.Entities;
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

  public async Task<User?> GetByIdAsync(Guid userId)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
  public async Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User> DeleteAsync(User user)
  {
    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
    return user;
  }

  // public async Task<User> AddListAsync(User user)
  // {
  //   _context.Users.Update(user);
  //   await _context.SaveChangesAsync();
  //   return user;
  // }
}
