using FluentValidation;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Helpers;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Repositories.EmployeeRepository;
using static TMS.Core.Common.Errors.ErrorMessages;
using static TMS.Core.Common.Employee;

namespace TMS.Application.Features.Employees.ValidationHelpers;

/// <summary>
/// Implements the methods to Validate the Employee Related Information.
/// </summary>
public class EmployeeValidator : IEmployeeValidator
{
    #region Fields

    /// <summary>
    /// The name of the helper used for logging.
    /// </summary>
    private const string HelperName = nameof(EmployeeValidator);

    /// <summary>
    /// Defines the Employee Repository for performing employee related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Logger instance for capturing Helper logs.
    /// </summary>
    private readonly ILogger<EmployeeValidator> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="EmployeeValidator"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/></param>
    /// <param name="logger">Defines the logger instance of Employee Validator</param>
    public EmployeeValidator(IEmployeeReadRepository employeeReadRepository, ILogger<EmployeeValidator> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _logger = logger;
    }

    #endregion

    #region Employee Validation Methods

    /// <inheritdoc cref="IEmployeeValidator.ValidateCreateEmployeeRequestAsync"/>
    public async Task ValidateCreateEmployeeRequestAsync(string newEmployeeNumber, string newEmailAddress,
        ValidationContext<CreateEmployeeRequest> context)
    {
        const string methodName = nameof(ValidateCreateEmployeeRequestAsync);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input : {EmployeeNumber} and {EmailAddress}",
            HelperName, methodName, newEmployeeNumber, newEmailAddress);

        var existingEmployee =
            await _employeeReadRepository.GetEmployeeByNumberOrEmail(newEmployeeNumber, newEmailAddress);

        if (existingEmployee.Count != 0)
        {
            ValidateEmployeeNumber(existingEmployee, newEmployeeNumber, context);
            ValidateEmployeeEmailAddress(existingEmployee, newEmailAddress, context);
        }

        _logger.LogInformation(
            "{Helper}.{Method} - Execution completed successfully with input : {EmployeeNumber} and {EmailAddress}",
            HelperName, methodName, newEmployeeNumber, newEmailAddress);
    }

    public async Task ValidateEmployeeEndDate<T>(
        string employeeTypeId, DateTimeOffset startDate, DateTimeOffset endDate, ValidationContext<T> context)
    {
        const string methodName = nameof(ValidateEmployeeEndDate);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input : {EmployeeTypeId} and {StartDate} and {EndDate} ",
            HelperName, methodName, employeeTypeId, startDate, endDate);

        var employeeType = await _employeeReadRepository.GetEmployeeType(employeeTypeId);

        switch (employeeType)
        {
            case { Type: Contractor or Intern } when !endDate.IsValidDateOffset():
                context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                    string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.EndDate),
                        endDate));
                break;
            case { Type: Contractor or Intern } when endDate < startDate.AddMonths(1):
                context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                    string.Format(ValidationMessages.EndDateTooSoon, nameof(CreateEmployeeRequest.EndDate),
                        startDate.AddMonths(1).Date.ToShortDateString()));
                break;
            default:
            {
                if (employeeType is not { Type: Contractor or Intern } && !endDate.IsValidDateOffset())
                    context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                        EmployeeValidationMessages.EndDateShouldBeNull);
                break;
            }
        }


        _logger.LogInformation("{Helper}.{Method} - Execution completed successfully", HelperName, methodName);
    }

    /// <inheritdoc cref="IEmployeeValidator.ValidateExcelEmployeeData"/>
    public async Task ValidateExcelEmployeeData(
        List<string> employeeIds, List<string> emails, ValidationContext<ImportEmployee> context)
    {
        const string methodName = nameof(ValidateExcelEmployeeData);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input : {EmployeeNumbers}, {Emails} ",
            HelperName, methodName, employeeIds, emails);

        var existingEmployees = 
            await _employeeReadRepository.GetEmployeesByEmailOrNumbers(string.Join(", ", employeeIds), string.Join(", ", emails));

        if (existingEmployees is { Count: > 0 })
        {
            if (existingEmployees.Select(e => e.EmployeeNumber).Intersect(employeeIds).ToList() is { Count: > 0 } existingEmployeeNumbers)
                context.AddFailure(nameof(ImportEmployeeRequest.EmployeeNumber),
                    string.Format(EmployeeValidationMessages.EmployeeNumbersInuse, string.Join(", ", existingEmployeeNumbers)));
            
            else if (existingEmployees.Select(e => e.Email).Intersect(emails).ToList() is { Count: > 0 } existingEmployeeEmails)
                context.AddFailure(nameof(ImportEmployeeRequest.Email),
                    string.Format(EmployeeValidationMessages.EmployeeEmailsInuse, string.Join(", ", existingEmployeeEmails)));
        }

        _logger.LogInformation("{Helper}.{Method} - Execution completed successfully", HelperName, methodName);
    }

    #endregion
    
    #region EmployeeType Validation Methods

    /// <inheritdoc cref="IEmployeeValidator.ValidateEmployeeTypeAsync"/>
    public async Task<bool> ValidateEmployeeTypeAsync(string employeeTypeId)
    {
        const string methodName = nameof(ValidateEmployeeTypeAsync);

        _logger.LogInformation("{Helper}.{Method} - Execution started successfully with input : {EmployeeTypeId} ",
            HelperName, methodName, employeeTypeId);

        var employeeType = await _employeeReadRepository.GetEmployeeType(employeeTypeId);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution completed successfully with output : {EmployeeType}",
            HelperName, methodName, employeeType.Type);

        return employeeType is { Type: "Contractor" or "Intern" };
    }

    /// <inheritdoc cref="IEmployeeValidator.ValidateEmployeeTypeNameAsync"/>
    public async Task ValidateEmployeeTypeNameAsync(
        string employeeTypeName, ValidationContext<CreateEmployeeTypeRequest> context)
    {
        const string methodName = nameof(ValidateEmployeeTypeAsync);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input EmployeeType : {EmployeeTypeI} ",
            employeeTypeName, HelperName, methodName);

        var employeeType = await _employeeReadRepository.GetEmployeeTypeByName(employeeTypeName);

        if (employeeType is not null)
            context.AddFailure(nameof(CreateEmployeeTypeRequest.Type),
                string.Format(EmployeeTypeValidationMessages.EmployeeTypeFound, employeeTypeName));

        _logger.LogInformation("{Helper}.{Method} - Execution completed successfully", HelperName, methodName);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Implements the functionality to Check if the employee number assigned during creation is already
    /// associated with another employee.
    /// </summary>
    /// <param name="employeeResponse">The existing employees with same employee number or email</param>
    /// <param name="newEmployeeNumber">he unique Number assigned to employee during creation.</param>
    /// <param name="context">The employee validation context to capture validation errors.</param>
    private void ValidateEmployeeNumber(
        List<EmployeeResponse> employeeResponse,
        string newEmployeeNumber,
        ValidationContext<CreateEmployeeRequest> context)
    {
        const string methodName = nameof(ValidateEmployeeNumber);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input : {EmployeeNumber} and {@EmployeeResponse}",
            HelperName, methodName, newEmployeeNumber, employeeResponse);

        var existingEmployee = employeeResponse.SingleOrDefault(x =>
            string.Equals(x.EmployeeNumber, newEmployeeNumber));

        if (existingEmployee is not null)
            context.AddFailure(nameof(CreateEmployeeRequest.EmployeeNumber),
                string.Format(EmployeeValidationMessages.EmployeeNumberInUse, newEmployeeNumber,
                    existingEmployee.Name));

        _logger.LogInformation(
            "{Helper}.{Method} - Execution completed successfully for output : {EmployeeNumber} and {@EmployeeResponse}",
            HelperName, methodName, newEmployeeNumber, employeeResponse);
    }

    /// <summary>
    /// Implements the functionality to Check if the email address assigned during creation is already
    /// associated with another employee.
    /// </summary>
    /// <param name="employeeResponse">The existing employees with same employee number or email</param>
    /// <param name="newEmployeeEmailAddress">The unique Email assigned to employee during creation.</param>
    /// <param name="context">The employee validation context to capture validation errors.</param>
    private void ValidateEmployeeEmailAddress(
        List<EmployeeResponse> employeeResponse,
        string newEmployeeEmailAddress,
        ValidationContext<CreateEmployeeRequest> context)
    {
        const string methodName = nameof(ValidateEmployeeEmailAddress);

        _logger.LogInformation(
            "{Helper}.{Method} - Execution started successfully with input : {EmailAddress} and {@EmployeeResponse}",
            HelperName, methodName, newEmployeeEmailAddress, employeeResponse);

        var existingEmployee = employeeResponse.SingleOrDefault(x =>
            string.Equals(x.Email, newEmployeeEmailAddress, StringComparison.OrdinalIgnoreCase));

        if (existingEmployee is not null)
            context.AddFailure(nameof(CreateEmployeeRequest.Email),
                string.Format(EmployeeValidationMessages.EmailInUse, newEmployeeEmailAddress));

        _logger.LogInformation(
            "{Helper}.{Method} - Execution completed successfully for input : {EmailAddress} and {@EmployeeResponse}",
            HelperName, methodName, newEmployeeEmailAddress, employeeResponse);
    }

    #endregion
}