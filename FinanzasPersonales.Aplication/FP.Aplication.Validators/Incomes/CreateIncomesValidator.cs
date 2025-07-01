using FinanciasPersonalesApiRest.DTOs.IncomesDTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FinanciasPersonalesApiRest.Validators.Incomes
{
    public class CreateIncomesValidator : AbstractValidator<CreateIncomesDTO>
    {
        public CreateIncomesValidator()
        {
            RuleFor(x => x.Name)
             .NotNull().WithMessage("El nombre no puede ser nulo.")
             .NotEmpty().WithMessage("El nombre no puede estar vacío.")
             .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("La descripción no puede ser nula.")
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(100).WithMessage("La descripción no puede tener más de 100 caracteres.");

            RuleFor(x => x.Amount)
                .NotNull().WithMessage("El monto no puede ser nulo.")
                .NotEmpty().WithMessage("El monto no puede estar vacío.")
                .GreaterThan(0).WithMessage("Todo monto debe ser mayor a 0.");

            RuleFor(x => x.Date)
                .NotNull().WithMessage("La fecha no puede ser nula.")
                .NotEmpty().WithMessage("La fecha no puede estar vacía.");



            RuleFor(x => x.IdTypeIncomes)
                .NotNull().WithMessage("El tipo de ingreso no puede ser nulo.")
                .NotEmpty().WithMessage("El tipo de ingreso no puede estar vacío.")
                .GreaterThan(0).WithMessage("El tipo de ingreso debe ser un valor mayor que 0.");

            //RuleFor(x => x.IdUser)
            //    .NotNull()
            //    .NotEmpty().WithMessage(" Usuario no autenticado ")
            //    .GreaterThan(0);

        }
      
    }
}
