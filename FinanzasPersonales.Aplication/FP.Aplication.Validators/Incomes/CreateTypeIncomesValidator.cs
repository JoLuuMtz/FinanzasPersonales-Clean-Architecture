using FinanciasPersonalesApiRest.DTOs.IncomesDTO;
using FluentValidation;

namespace FinanciasPersonalesApiRest.Validators.Incomes
{
    public class CreateTypeIncomesValidator : AbstractValidator<CreateTypeIncomesDTO>

    {
        public CreateTypeIncomesValidator()
        {
            RuleFor(typeIncomes => typeIncomes.Name)
                .NotNull().WithMessage("The name is required")
                .MaximumLength(50).WithMessage("The description must be less than 50 characters");

            RuleFor(typeIncomes => typeIncomes.Description)
                .NotNull().WithMessage("The description is required")
                .MaximumLength(500).WithMessage("The description must be less than 500 characters");
        }
    }
}
