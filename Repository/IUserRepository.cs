using UsersAPI.Models;

namespace UsersAPI.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(int page, int pageSize);
    Task<(IEnumerable<User> users, int totalCount)> GetAllWithCountAsync(int page, int pageSize);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync(int page, int pageSize);
    Task<(IEnumerable<User> users, int totalCount)> GetActiveUsersWithCountAsync(int page, int pageSize);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<bool> EmailExistsAsync(string email);
}
