
using FluentValidation;

namespace FinanzasPersonales.Aplication
{
    public class UpdateIncomesValidator : AbstractValidator<UpdateIncomesDTO>
    {
          public UpdateIncomesValidator()
        {

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Todo monto debe ser mayor a 0");

            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty();

        }


    }
}
