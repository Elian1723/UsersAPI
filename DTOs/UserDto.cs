namespace UsersAPI;

public class UserDto
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}".Trim();

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
    
     public int Age => DateTime.Now.Year - DateOfBirth.Year;

    public bool IsActive { get; set; }
}
