using FluentValidation;
using Recommendation.API.Interfaces;

namespace Recommendation.API.Validations
{
    public class SOrderItemValidator: AbstractValidator<IOrderItem>
    {
        public SOrderItemValidator()
        {
            RuleFor(orderItem => orderItem.CreateOrderItemModel).NotNull().NotEmpty();
            RuleFor(orderItem => orderItem.CreateOrderItemModel.Id).GreaterThan(0);
            RuleFor(orderItem => orderItem.CreateOrderItemModel.OrderId).GreaterThan(0);
            RuleFor(orderItem => orderItem.CreateOrderItemModel.Quantity).GreaterThan(0);
            RuleFor(orderItem => orderItem.CreateOrderItemModel.ProductId).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0");

        }
    }
}
