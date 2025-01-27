using Cars.Models.DTO;
using FluentValidation;

namespace Cars.Validations
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
           
            RuleFor(x => x.Model)
                .NotNull().WithMessage("Model cannot be null.")
                .NotEmpty().WithMessage("Model cannot be empty.")
                .MaximumLength(50).WithMessage("Model cannot be longer than 50 characters.");

            RuleFor(x => x.Year)
                .GreaterThan(0).WithMessage("Year must be greater than 0.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Year cannot be greater than the current year.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.CustomerId)
                .NotNull().WithMessage("CustomerId cannot be null.")
                .NotEmpty().WithMessage("CustomerId cannot be empty.");
        }
    }
}
