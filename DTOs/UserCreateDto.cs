using System.ComponentModel.DataAnnotations;

namespace UsersAPI;

public class UserCreateDto
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es requerido")]
    [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "El correo electrónico es requerido")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder 50 caracteres")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El teléfono es requerido")]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    [StringLength(15, ErrorMessage = "El teléfono no puede exceder 15 caracteres")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    public DateTime DateOfBirth { get; set; }
}
