using FluentValidation;
using TMS.Core.Common.Errors;

namespace TMS.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="string"/>.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class EmailValidator : AbstractValidator<string>
{
    /// <summary>
    /// Initializes the new instance of <see cref="EmailValidator"/>
    /// Defines validation rules for Email.
    /// </summary>
    public EmailValidator()
    {
        RuleFor(x => x)
            .EmailAddress()
            .WithMessage(x => string.Format(ErrorMessages.ValidationMessages.InvalidEmail, "Email"));
    }
}