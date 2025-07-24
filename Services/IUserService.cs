using UsersAPI.DTOs;

namespace UsersAPI.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync(int page = 0, int pageSize = 10);
    Task<(IEnumerable<UserDto> users, int totalCount)> GetAllUsersWithCountAsync(int page = 0, int pageSize = 10);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetActiveUsersAsync(int page = 0, int pageSize = 10);
    Task<(IEnumerable<UserDto> users, int totalCount)> GetActiveUsersWithCountAsync(int page = 0, int pageSize = 10);
    Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> ActivateUserAsync(int id);
    Task<bool> DeactivateUserAsync(int id);
}
