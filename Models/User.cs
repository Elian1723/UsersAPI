using System;
using System.Collections.Generic;

namespace UsersAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string SecondName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public bool IsActive { get; set; }
}
