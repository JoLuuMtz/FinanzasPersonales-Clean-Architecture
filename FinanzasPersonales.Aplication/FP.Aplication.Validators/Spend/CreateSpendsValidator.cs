using FinanciasPersonalesApiRest.DTOs.SpendsDTO;
using FluentValidation;

namespace FinanciasPersonalesApiRest.Validators.Spend
{
    public class CreateSpendsValidator : AbstractValidator<CreatedSpendDTO>
    {

        public CreateSpendsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacio")
                .NotNull().WithMessage("El nombre no puede ser nulo")
                .MaximumLength(50).WithMessage("El nombre tiene mas de muchos caracteres");
                

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("El monto no puede estar vacio")
                .GreaterThan(0).WithMessage(" el monto debe ser mayor a 0 ")
                .WithMessage("El monto debe ser mayor a 0");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull().WithMessage("La descripcion debe ser vacia ")
                .WithMessage("La descripción no puede estar vacia")
                .MaximumLength(500)
                .WithMessage("La descripción no puede tener mas de 100 caracteres");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("La fecha no puede estar vacia");


        }
    }
}
