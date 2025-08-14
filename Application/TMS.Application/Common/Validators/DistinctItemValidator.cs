using FluentValidation;

namespace TMS.Application.Common.Validators;

/// <summary>
/// Validator for a list of items to ensure that all items in the list have distinct values for a specified property.
/// </summary>
/// <typeparam name="T">Defines the class.</typeparam>
/// <typeparam name="TProperty">Defines the Property Name.</typeparam>
public class DistinctItemValidator<T, TProperty> : AbstractValidator<List<T>>
{
    /// <summary>
    /// Initializes the new instance of <see cref="DistinctItemValidator{T, TProperty}"/>
    /// </summary>
    /// <param name="propertyName">Defines the <see cref="propertyName"/></param>
    /// <param name="propertySelector">Defines the <see cref="propertySelector"/>.</param>
    public DistinctItemValidator(string propertyName, Func<T, TProperty> propertySelector)
    {
        RuleFor(x => x)
            .Must(x => x.GroupBy(propertySelector).All(group => group.Count() == 1))
            .WithMessage(x => $"Duplicate {propertyName}s found : {string.Join(", ", x.GroupBy(propertySelector).Select(g => g.Key))}");
    }
}