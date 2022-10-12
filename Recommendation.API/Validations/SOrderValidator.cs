using FluentValidation;
using Recommendation.API.Interfaces;

namespace Recommendation.API.Validations
{
    public class SOrderValidator: AbstractValidator<IOrder>
    {
        public SOrderValidator()
        {
            RuleFor(order => order.CreateOrderModel).NotNull().NotEmpty();
            RuleFor(order => order.CreateOrderModel.OrderId).GreaterThan(0);
            RuleFor(order => order.CreateOrderModel.UserId).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0");
        }
    }
}
