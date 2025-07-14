using UsersAPI.Models;

namespace UsersAPI;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}
