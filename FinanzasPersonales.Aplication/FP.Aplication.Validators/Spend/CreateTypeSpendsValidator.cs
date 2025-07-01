using FinanciasPersonalesApiRest.DTOs.SpendsDTO;
using FluentValidation;

namespace FinanciasPersonalesApiRest.Validators.Spend
{
    public class CreateTypeSpendsValidator : AbstractValidator<CreateTypeSpendDTO>
    {
        public CreateTypeSpendsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacio")
                .NotNull().WithMessage("El nombre no puede ser nulo")
                .MaximumLength(50).WithMessage("El nombre tiene mas de muchos caracteres");


            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("La descripción no puede estar vacia")
                .MaximumLength(500)
                .WithMessage("La descripción no puede tener mas de 100 caracteres");
        }
    }
}
