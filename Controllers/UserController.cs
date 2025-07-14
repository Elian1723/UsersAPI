using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Models;

namespace UsersAPI.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UsersAPIDbContext _context;

        public UserController(UsersAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            return Ok(await _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                DateOfBirth = u.DateOfBirth,
                IsActive = u.IsActive
            }).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }
            return Ok(new UserDto
            {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Phone = users.Phone,
                DateOfBirth = users.DateOfBirth,
                IsActive = users.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest("User data is null.");
            }

            var user = new User
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                Phone = userCreateDto.Phone,
                DateOfBirth = userCreateDto.DateOfBirth,
                IsActive = true
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound("User not found");

            if (userUpdateDto == null) return BadRequest("User data is null");

            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Email = userUpdateDto.Email;
            user.Phone = userUpdateDto.Phone;
            user.DateOfBirth = userUpdateDto.DateOfBirth;

            await _context.SaveChangesAsync();

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
            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
