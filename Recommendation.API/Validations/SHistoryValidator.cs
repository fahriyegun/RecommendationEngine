using FluentValidation;
using Recommendation.API.Interfaces;

namespace Recommendation.API.Validations
{
    public class SHistoryValidator: AbstractValidator<IHistory>
    {
        public SHistoryValidator()
        {
            RuleFor(history => history.UserId).NotNull().NotEmpty().NotEqual("0").MinimumLength(1).MaximumLength(50)
                .When(history => history.CreateHistoryModel == null && history.DeleteHistoryModel == null);


            RuleFor(history => history.CreateHistoryModel).NotNull()
                .When(history => history.UserId == null && history.DeleteHistoryModel == null);
            RuleFor(history => history.CreateHistoryModel.messageid).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                .When(history => history.UserId == null && history.DeleteHistoryModel == null);
            RuleFor(history => history.CreateHistoryModel.userid).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                .When(history => history.UserId == null && history.DeleteHistoryModel == null);
            RuleFor(history => history.CreateHistoryModel.properties).NotNull()
                .When(history => history.UserId == null && history.DeleteHistoryModel == null);
            RuleFor(history => history.CreateHistoryModel.properties.productid).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                .When(history => history.UserId == null && history.DeleteHistoryModel == null); 


            RuleFor(history => history.DeleteHistoryModel).NotNull()
                 .When(history => history.UserId == null && history.CreateHistoryModel == null);
            RuleFor(history => history.DeleteHistoryModel.productid).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                 .When(history => history.UserId == null && history.CreateHistoryModel == null);
            RuleFor(history => history.DeleteHistoryModel.userid).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50).NotEqual("0")
                 .When(history => history.UserId == null && history.CreateHistoryModel == null);

        }
    }
}
