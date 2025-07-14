namespace UsersAPI;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
    Task<bool> DeleteUserAsync(int id);
}
