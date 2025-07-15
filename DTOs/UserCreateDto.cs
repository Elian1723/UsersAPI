using System.ComponentModel.DataAnnotations;

namespace UsersAPI.DTOs;

public class UserCreateDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
