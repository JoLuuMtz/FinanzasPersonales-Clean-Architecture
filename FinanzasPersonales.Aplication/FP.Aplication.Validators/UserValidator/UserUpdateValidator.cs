using FinanciasPersonalesApiRest.DTOs.UserDTO;
using FluentValidation;
using System.Net.Quic;

namespace FinanciasPersonalesApiRest.Validators.UserValidator
{
    public class UserUpdateValidator : AbstractValidator<UpdateUserDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.Name)
             .NotNull().NotEmpty()
             .MinimumLength(3)
             .MaximumLength(50)
             .Must(name => !name.Contains("'") && !name.Contains("--"))
             .WithMessage("Nombre no válido. Inserte un nombre real");

            RuleFor(user => user.Email)
            .NotNull().NotEmpty()
            .EmailAddress()
            .WithMessage("Correo no válido.");

            RuleFor(user => user.LastName)
            .NotNull().NotEmpty()
            .MinimumLength(5)
            .MaximumLength(15)
            .Must(lastName => !lastName.Contains("'") && !lastName.Contains("--"))
            .WithMessage("El apellido tiene caracteres especiales");
        }
    }
}
