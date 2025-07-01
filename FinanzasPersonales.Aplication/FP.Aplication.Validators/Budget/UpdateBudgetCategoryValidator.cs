using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FluentValidation;

namespace FinanciasPersonalesApiRest.Validators.Budget
{
    public class UpdateBudgetCategoryValidator : AbstractValidator<UpdateBudgetCategoryDTO>
    {
        public UpdateBudgetCategoryValidator()
        {
            RuleFor(b => b.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 50).WithMessage("Name must be between 3 and 50 characters");

            RuleFor(b => b.Description)
                .NotNull().WithMessage("Description is required")
                .NotEmpty().WithMessage("Description is required")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters");

            RuleFor(b => b.Amount)
                .NotNull().WithMessage("Amount is required")
                .NotEmpty().WithMessage("Amount is required")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
