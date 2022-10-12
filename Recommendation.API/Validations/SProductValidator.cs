using FluentValidation;
using Recommendation.API.Interfaces;
using System.Numerics;

namespace Recommendation.API.Validations
{
    public class SProductValidator: AbstractValidator<IProduct>
    {
        public SProductValidator()
        {
            RuleFor(product => product.Userid).NotEmpty().NotEqual("0").When(product => product.CreateProductModel == null);

            RuleFor(product => product.CreateProductModel).NotNull().NotEmpty().When(product => product.Userid == null);
            RuleFor(product => product.CreateProductModel.productId).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                .When(product => product.Userid == null);
            RuleFor(product => product.CreateProductModel.categoryId).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                .When(product => product.Userid == null);
        }
    }
}
