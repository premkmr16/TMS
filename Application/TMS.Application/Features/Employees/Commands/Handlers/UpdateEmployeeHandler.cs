using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Application.Repositories.EmployeeRepository;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Commands.Handlers;

/// <summary>
/// Handles the process of Updating existing employee.
/// </summary>
public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, UpdateEmployeeResponse>
{
    #region Fields

    /// <summary>
    /// The name of the Handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(UpdateEmployeeHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeWriteRepository _employeeWriteRepository;

    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Defines the validator for validating <see cref="UpdateEmployeeRequest"/>.
    /// </summary>
    private readonly IValidator<UpdateEmployeeRequest> _updateEmployeeRequestValidator;

    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Logger instance for capturing <see cref="UpdateEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<UpdateEmployeeHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="UpdateEmployeeHandler"/>.
    /// </summary>
    /// <param name="employeeWriteRepository">Defines the Employee Repository <see cref="IEmployeeWriteRepository"/>.</param>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="updateEmployeeRequestValidator">Defines the validator of <see cref="UpdateEmployeeRequest"/>.</param>
    /// <param name="mapper">Defines the Mapper of Employee <see cref="IMapper"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="UpdateEmployeeHandler"/>.</param>
    public UpdateEmployeeHandler(
        IEmployeeWriteRepository employeeWriteRepository,
        IEmployeeReadRepository employeeReadRepository,
        IValidator<UpdateEmployeeRequest> updateEmployeeRequestValidator,
        IMapper mapper,
        ILogger<UpdateEmployeeHandler> logger)
    {
        _employeeWriteRepository = employeeWriteRepository;
        _employeeReadRepository = employeeReadRepository;
        _updateEmployeeRequestValidator = updateEmployeeRequestValidator;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to update existing employee in database.
    /// </summary>
    /// <param name="request">The request containing employee details to be updated.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="UpdateEmployeeResponse"/> containing the result of update operation.</returns>
    public async Task<UpdateEmployeeResponse> Handle(UpdateEmployee request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(UpdateEmployee);

        _logger.LogInformation("{Handler}.{Method} - Execution started successfully with input : {@Employee}",
            HandlerName, methodName, request.Employee);

        var updateEmployeeRequestValidatorResult = 
            await _updateEmployeeRequestValidator.ValidateAsync(request.Employee, cancellationToken);

        if (!updateEmployeeRequestValidatorResult.IsValid)
            throw new ValidationException(updateEmployeeRequestValidatorResult.Errors);

        var existingEmployee = await _employeeReadRepository.GetEmployeeWithoutTypeName(request.Employee.Id);

        if (existingEmployee is null) 
            throw new InvalidOperationException(string.Format(EmployeeValidationMessages.EmployeeNotFound, request.Employee.Id));

        _mapper.Map(request.Employee, existingEmployee);

        var updatedEmployee = await _employeeWriteRepository.UpdateEmployee(existingEmployee);
        var response = _mapper.Map<UpdateEmployeeResponse>(updatedEmployee);

        _logger.LogInformation("{Handler}.{Method} - Execution completed successfully with output : {@Employee}",
            HandlerName, methodName, response);

        return response;
    }

    #endregion  
}