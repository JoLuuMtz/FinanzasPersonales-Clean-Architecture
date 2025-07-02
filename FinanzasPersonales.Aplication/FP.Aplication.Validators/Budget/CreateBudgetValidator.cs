
using FluentValidation;

namespace FinanzasPersonales.Aplication
{
    public class CreateBudgetValidator : AbstractValidator<CreateBudgetDTO>
    {
        public CreateBudgetValidator()
        {
            RuleFor(budget => budget.Name)
                .NotEmpty().WithMessage("The name is required")   
                .NotNull().WithMessage("The name is required")
                .MaximumLength(50).WithMessage("The name must be less than 50 characters");

            RuleFor(budget => budget.Description)
                .NotEmpty().WithMessage("The description is required") 
                .NotNull().WithMessage("The description is required")
                .MaximumLength(500).WithMessage("The description must be less than 500 characters");

         

            ;
        }
    }

}


