using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.DTOs;
using UsersAPI.Services;

namespace UsersAPI.Controllers
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
        public async Task<IActionResult> GetAllUsers(int page = 0, int pageSize = 10)
        {
            try
            {
                var (users, totalCount) = await _userService.GetAllUsersWithCountAsync(page, pageSize);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                Response.Headers["X-Current-Page"] = page.ToString();
                Response.Headers["X-Page-Size"] = pageSize.ToString();
                Response.Headers["X-Total"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public async Task<IActionResult> GetActiveUsers(int page = 0, int pageSize = 10)
        {
            try
            {
                var (users, totalCount) = await _userService.GetActiveUsersWithCountAsync(page, pageSize);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                Response.Headers["X-Current-Page"] = page.ToString();
                Response.Headers["X-Page-Size"] = pageSize.ToString();
                Response.Headers["X-Total"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                var validationResult = await _userCreateValidator.ValidateAsync(userCreateDto);

                if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

                var user = await _userService.CreateUserAsync(userCreateDto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
