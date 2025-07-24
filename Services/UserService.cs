using AutoMapper;
using UsersAPI.DTOs;
using UsersAPI.Models;
using UsersAPI.Repository;

namespace UsersAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int page = 0, int pageSize = 10)
    {
        if (page < 0)
        {
            throw new InvalidOperationException("The page value should not be a negative number");
        }

        if (pageSize < 0)
        {
            throw new InvalidOperationException("The pageSize value should not be a negative number");
        }

        var users = await _userRepository.GetAllAsync(page, pageSize);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<(IEnumerable<UserDto> users, int totalCount)> GetAllUsersWithCountAsync(int page = 0, int pageSize = 10)
    {
        if (page < 0)
        {
            throw new InvalidOperationException("The page value should not be a negative number");
        }

        if (pageSize < 0)
        {
            throw new InvalidOperationException("The pageSize value should not be a negative number");
        }

        var (users, totalCount) = await _userRepository.GetAllWithCountAsync(page, pageSize);

        return (_mapper.Map<IEnumerable<UserDto>>(users), totalCount);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email.Trim())) return null;

        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetActiveUsersAsync(int page = 0, int pageSize = 10)
    {
        if (page < 0)
        {
            throw new InvalidOperationException("The page value should not be a negative number");
        }

        if (pageSize < 0)
        {
            throw new InvalidOperationException("The pageSize value should not be a negative number");
        }
        
        var users = await _userRepository.GetActiveUsersAsync(page, pageSize);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<(IEnumerable<UserDto> users, int totalCount)> GetActiveUsersWithCountAsync(int page = 0, int pageSize = 10)
    {
        if (page < 0)
        {
            throw new InvalidOperationException("The page value should not be a negative number");
        }

        if (pageSize < 0)
        {
            throw new InvalidOperationException("The pageSize value should not be a negative number");
        }
        
        var (users, totalCount) = await _userRepository.GetActiveUsersWithCountAsync(page, pageSize);

        return (_mapper.Map<IEnumerable<UserDto>>(users), totalCount);
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        if (await _userRepository.EmailExistsAsync(userCreateDto.Email)) throw new InvalidOperationException($"The email {userCreateDto.Email} already exists");

        var user = _mapper.Map<User>(userCreateDto);

        var userCreated = await _userRepository.CreateAsync(user);

        return _mapper.Map<UserDto>(userCreated);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new InvalidOperationException("User not found");

        if (await _userRepository.EmailExistsAsync(userUpdateDto.Email)) throw new InvalidOperationException($"The email {userUpdateDto.Email} already exists");

        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.Email = userUpdateDto.Email;
        user.Phone = userUpdateDto.Phone;
        user.DateOfBirth = userUpdateDto.DateOfBirth;

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
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
}
