using FluentValidation;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Services.PipelineBehaviors.ValidationBehavior.Extensions;
using OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Services.PipelineBehaviors.ValidationBehavior.Validators
{
    public sealed class GetAllMerchPackByEmployeeQueryValidator : AbstractValidator<GetAllMerchPackByEmployeeQuery>
    {
        public GetAllMerchPackByEmployeeQueryValidator()
        {
            RuleFor(x => x.Email).MustBeValidObject(Email.Create);
        }
    }
}