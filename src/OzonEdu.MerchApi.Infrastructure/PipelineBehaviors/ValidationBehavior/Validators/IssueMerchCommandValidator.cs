using FluentValidation;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior.Extensions;

namespace OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior.Validators
{
    public sealed class IssueMerchCommandValidator : AbstractValidator<IssueMerchCommand>
    {
        public IssueMerchCommandValidator()
        {
            RuleFor(x => x.Employee.FirstName).MustBeValidObject(FirstName.Create);
            RuleFor(x => x.Employee.LastName).MustBeValidObject(LastName.Create);
            RuleFor(x => x.Employee.MiddleName).MustBeValidObject(MiddleName.Create);
            RuleFor(x => x.Employee.Email).MustBeValidObject(Email.Create);
            RuleFor(x => x.FromType).MustBeInEnumeration<IssueMerchCommand, MerchRequestFromType>();
            RuleFor(x => x.MerchPackTypeId).GreaterThanOrEqualTo(1);
        }
    }
}