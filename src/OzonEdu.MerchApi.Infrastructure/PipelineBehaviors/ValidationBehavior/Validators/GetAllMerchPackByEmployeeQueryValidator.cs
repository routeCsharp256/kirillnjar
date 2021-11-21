using FluentValidation;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior.Extensions;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior.Validators
{
    public sealed class GetAllMerchPackByEmployeeQueryValidator : AbstractValidator<GetAllMerchPackByEmployeeQuery>
    {
        public GetAllMerchPackByEmployeeQueryValidator()
        {
            RuleFor(x => x.Email).MustBeValidObject(Email.Create);
        }
    }
}