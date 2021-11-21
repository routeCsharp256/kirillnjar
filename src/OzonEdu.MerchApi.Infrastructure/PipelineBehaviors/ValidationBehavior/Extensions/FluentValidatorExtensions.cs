using System;
using System.Linq;
using FluentValidation;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidObject<T, TValueObject>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, TValueObject> factoryMethod) where TValueObject : ValueObject
            => (IRuleBuilderOptions<T, string>) ruleBuilder.Custom((value, context) =>
            {
                try
                {
                    factoryMethod(value);
                }
                catch (Exception e)
                {
                    context.AddFailure(e.Message);
                }
            });
        
        public static IRuleBuilderOptions<T, int> MustBeInEnumeration<T, TEnumeration>(
            this IRuleBuilder<T, int> ruleBuilder) where TEnumeration : Enumeration
            => (IRuleBuilderOptions<T, int>) ruleBuilder.Custom((value, context) =>
            {
                try
                {
                    var founded = Enumeration
                        .GetAll<TEnumeration>()
                        .FirstOrDefault(it => it.Id.Equals(value));
                    if (founded == default)
                        context.AddFailure($"{value} not in enum {typeof(TEnumeration).Name}");
                }
                catch (Exception e)
                {
                    context.AddFailure(e.Message);
                }
            });
    }
}