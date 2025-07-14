using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                DateOfBirth = u.DateOfBirth,
                IsActive = u.IsActive
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return NotFound();

            return Ok(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null) return BadRequest("User data is null.");

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

            return Ok(new UserDto
            {
                Id = userCreated.Id,
                FirstName = userCreated.FirstName,
                LastName = userCreated.LastName,
                Email = userCreated.Email,
                Phone = userCreated.Phone,
                DateOfBirth = userCreated.DateOfBirth,
                IsActive = userCreated.IsActive
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return NotFound("User not found");

            if (userUpdateDto == null) return BadRequest("User data is null");

            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Email = userUpdateDto.Email;
            user.Phone = userUpdateDto.Phone;
            user.DateOfBirth = userUpdateDto.DateOfBirth;

            await _userRepository.UpdateAsync(user);

            return Ok(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return NotFound("User not found");

            await _userRepository.DeleteAsync(user.Id);

            return NoContent();
        }
    }
}
