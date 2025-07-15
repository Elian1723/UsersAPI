using FluentValidation;

namespace UsersAPI.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.FirstName).Length(2, 50).WithMessage("First name must be between 2 and 50 characters");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.LastName).Length(2, 50).WithMessage("Last name must be between 2 and 50 characters");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Email).Length(2, 50).WithMessage("Email must be between 2 and 50 characters");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        RuleFor(x => x.Phone).Length(2, 15).WithMessage("Phone number must be between 2 and 15 characters");

        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
    }
}
