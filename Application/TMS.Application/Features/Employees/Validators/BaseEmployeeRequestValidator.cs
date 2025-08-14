using FluentValidation;
using TMS.Application.Common.Helpers;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="BaseEmployeeContract"/> class.
/// </summary>
/// <typeparam name="T">Defines a class used to validate fields inherited from a base class.</typeparam>
public class BaseEmployeeContractValidator<T> : AbstractValidator<T> where T : BaseEmployeeContract
{
    /// <summary>
    /// Initializes the new instance of <see cref="BaseEmployeeContractValidator{T}"/>
    /// </summary>
    protected BaseEmployeeContractValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(baseEmployeeRequest => baseEmployeeRequest.Name)
            .Must(employeeName => !string.IsNullOrWhiteSpace(employeeName))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(BaseEmployeeContract.Name)));

        RuleFor(baseEmployeeRequest => baseEmployeeRequest.DateOfBirth)
            .Must(dateOfBirth => dateOfBirth.IsValidDate())
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(BaseEmployeeContract.DateOfBirth)))
            .Must(dateOfBirth => DateTime.Today >= dateOfBirth.AddYears(18))
            .WithMessage(EmployeeValidationMessages.InvalidDateOfBirth);

        RuleFor(baseEmployeeRequest => baseEmployeeRequest.EmployeeTypeId)
            .Must(employeeTypeid => !string.IsNullOrWhiteSpace(employeeTypeid))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(BaseEmployeeContract.EmployeeTypeId)))
            .SetValidator(new UlidValidator());
        
        RuleFor(baseEmployeeRequest => baseEmployeeRequest.StartDate)
            .Must(startDate => startDate != DateTimeOffset.MinValue && startDate != DateTimeOffset.MaxValue)
            .WithMessage(baseEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(BaseEmployeeContract.StartDate), baseEmployeeRequest.StartDate));
        
        RuleFor(baseEmployeeRequest => baseEmployeeRequest.Phone)
            .Must(phone => !string.IsNullOrWhiteSpace(phone))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(BaseEmployeeContract.Phone)))
            .Length(10)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(BaseEmployeeContract.Phone)))
            .Matches(RegexValidation.PhoneNumberRegexPattern)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(BaseEmployeeContract.Phone)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    protected virtual bool ShouldValidateEmployeeTypeId(T instance) => true;
}