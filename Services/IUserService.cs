using UsersAPI.Models;

namespace UsersAPI;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetActiveUsersAsync();
    Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> ActivateUserAsync(int id);
    Task<bool> DeactivateUserAsync(int id);
    Task<bool> ExistsEmailAsync(string email);
}
