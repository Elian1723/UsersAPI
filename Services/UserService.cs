using Microsoft.AspNetCore.Http.HttpResults;
using UsersAPI.Models;

namespace UsersAPI;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Phone = u.Phone,
            DateOfBirth = u.DateOfBirth,
            IsActive = u.IsActive
        });
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive
        };
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email.Trim())) return null;

        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive
        };
    }

    public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
    {
        var users = await _userRepository.GetActiveUsersAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Phone = u.Phone,
            DateOfBirth = u.DateOfBirth,
            IsActive = u.IsActive
        });
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var user = new User
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email,
            Phone = userCreateDto.Phone,
            DateOfBirth = userCreateDto.DateOfBirth,
            IsActive = true
        };

        var userCreated = await _userRepository.CreateAsync(user);

        return new UserDto
        {
            Id = userCreated.Id,
            FirstName = userCreated.FirstName,
            LastName = userCreated.LastName,
            Email = userCreated.Email,
            Phone = userCreated.Phone,
            DateOfBirth = userCreated.DateOfBirth,
            IsActive = userCreated.IsActive
        };
    }

    public async Task<UserDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new InvalidOperationException("User not found");

        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.Email = userUpdateDto.Email;
        user.Phone = userUpdateDto.Phone;
        user.DateOfBirth = userUpdateDto.DateOfBirth;

        await _userRepository.UpdateAsync(user);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive
        };
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) return false;

        bool deleted = await _userRepository.DeleteAsync(user.Id);

        return deleted;
    }

    public async Task<bool> ActivateUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null || user.IsActive) return false;

        user.IsActive = true;
        await _userRepository.UpdateAsync(user);

        return true;
    }
    public async Task<bool> DeactivateUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null || !user.IsActive) return false;

        user.IsActive = false;
        await _userRepository.UpdateAsync(user);

        return true;
    }
    public async Task<bool> ExistsEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email.Trim())) return false;

        var user = await _userRepository.GetByEmailAsync(email);

        return user != null;
    }
}
