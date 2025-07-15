using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDto> _userCreateValidator;
        private readonly IValidator<UserUpdateDto> _userUpdateValidator;

        public UserController(IUserService userService, IValidator<UserCreateDto> userCreateValidator, IValidator<UserUpdateDto> userUpdateValidator)
        {
            _userService = userService;
            _userCreateValidator = userCreateValidator;
            _userUpdateValidator = userUpdateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email.Trim())) return BadRequest("Email is required");

            var user = await _userService.GetUserByEmailAsync(email);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var validationResult = await _userCreateValidator.ValidateAsync(userCreateDto);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var user = await _userService.CreateUserAsync(userCreateDto);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var validationResult = await _userUpdateValidator.ValidateAsync(userUpdateDto);

                if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

                var user = await _userService.UpdateUserAsync(id, userUpdateDto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            bool activated = await _userService.ActivateUserAsync(id);

            return activated ? NoContent() : NotFound();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            bool deactivated = await _userService.DeactivateUserAsync(id);

            return deactivated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _userService.DeleteUserAsync(id);

            if (!deleted) return NotFound("User not found");

            return NoContent();
        }
    }
}
