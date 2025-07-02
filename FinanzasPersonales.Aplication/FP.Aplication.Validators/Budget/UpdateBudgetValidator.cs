
using FluentValidation;

namespace FinanzasPersonales.Aplication
{
    public class UpdateBudgetValidator : AbstractValidator<UpdateBudgetDTO>
    {
        public UpdateBudgetValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name can't be longer than 50 characters");

            RuleFor(b => b.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(100)
                .WithMessage("Description can't be longer than 100 characters");
        }
    }
}
