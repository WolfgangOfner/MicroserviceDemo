using FluentValidation;
using OrderApi.Models.v1;

namespace OrderApi.Validators.v1
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.CustomerFullName)
                .NotNull()
                .WithMessage("The customer name must be at least 2 character long");
            RuleFor(x => x.CustomerFullName)
                .MinimumLength(2).WithMessage("The customer name must be at least 2 character long");
        }
    }
}