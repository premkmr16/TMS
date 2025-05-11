using FluentValidation;

namespace TMS.Application.Common.Validators;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TProperty"></typeparam>
public class DistinctItemValidator<T, TProperty> : AbstractValidator<List<T>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertySelector"></param>
    public DistinctItemValidator(string propertyName, Func<T, TProperty> propertySelector)
    {
        RuleFor(x => x)
            .Must(x => x.GroupBy(propertySelector).All(group => group.Count() == 1))
            .WithMessage(x => $"Duplicate {propertyName}s found : {string.Join(", ", x.GroupBy(propertySelector).Select(g => g.Key))}");
    }
}