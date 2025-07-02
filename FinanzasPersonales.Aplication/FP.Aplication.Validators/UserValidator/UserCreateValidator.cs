
using FluentValidation;


namespace FinanzasPersonales.Aplication
{
    public class UserCreateValidator : AbstractValidator<RegisterUserDTO>
    {

        public UserCreateValidator()
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

            // Validación de DNI
            RuleFor(user => user.DNI)
                .NotNull().NotEmpty()
                .MinimumLength(5)
                .MaximumLength(15)
                .WithMessage("DNI es requerido y debe tener entre 5 y 15 caracteres");

            RuleFor(user => user.Password)
                .NotNull().NotEmpty()
                .MinimumLength(8)
                .MaximumLength(50)
                .Matches(@"[!@#$%^&*(),.?""{}|<>]")
                .WithMessage("La contraseña debe contener al menos un carácter especial.")
                .Matches(@"\d")
                .WithMessage("La contraseña debe tener al menos un número.")
                .Matches(@"[A-Z]")
                .WithMessage("La contraseña debe tener al menos una letra mayúscula.");

            RuleFor(user => user.LastName)
                .NotNull().NotEmpty()
                .MinimumLength(5)
                .MaximumLength(15)
                .Must(lastName => !lastName.Contains("'") && !lastName.Contains("--"))
                .WithMessage("El apellido tiene caracteres especiales");

        }
    }
}
