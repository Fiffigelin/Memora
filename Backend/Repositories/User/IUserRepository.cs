

using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IUserRepository
{
  Task AddAsync(User user);
  Task<bool> ExistsByUsernameAsync(string username);
  Task<bool> ExistsByEmailAsync(string email);
  Task<IEnumerable<User>> GetAllUsersAsync();
  Task<User?> GetByIdAsync(Guid userId);
  Task<User> DeleteAsync(User user);
  Task<User?> GetByEmailAsync(string email);
  // Task<User> AddListAsync(User user);
  // User? GetByUsername(string username);
  // void Update(User user);
}
