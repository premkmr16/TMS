using FluentValidation;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="string"/>.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class UlidValidator : AbstractValidator<string>
{
    /// <summary>
    /// Initializes the new instance of <see cref="UlidValidator"/>
    /// Defines validation rules for Ulid.
    /// </summary>
    public UlidValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(id => id)
            .Length(26)
            .WithMessage(string.Format(ValidationMessages.InvalidStringLength, "Ulid"))
            .Matches(RegexValidation.StringRegexPattern)
            .WithMessage(id => string.Format(ValidationMessages.InvalidStringFormat, "Ulid", id));
    }
}