using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Commands.Handlers;

/// <summary>
/// Handles the process of adding a new employee.
/// </summary>
public class AddEmployeeHandler : IRequestHandler<AddEmployee, CreateEmployeeResponse>
{
    #region Fields

    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(AddEmployeeHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeWriteRepository _employeeWriteRepository;
    
    /// <summary>
    /// Defines the validator for validating <see cref="CreateEmployeeRequest"/>.
    /// </summary>
    private readonly IValidator<CreateEmployeeRequest> _createEmployeeRequestValidator;

    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="AddEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<AddEmployeeHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="AddEmployeeHandler"/>
    /// </summary>
    /// <param name="employeeWriteRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="createEmployeeRequestValidator">Defines the validator of <see cref="CreateEmployeeRequest"/>.</param>
    /// <param name="mapper">Defines the Mapper of Employee <see cref="IMapper"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="AddEmployeeHandler"/>.</param>
    public AddEmployeeHandler(
        IEmployeeWriteRepository employeeWriteRepository,
        IValidator<CreateEmployeeRequest> createEmployeeRequestValidator,
        IMapper mapper,
        ILogger<AddEmployeeHandler> logger)
    {
        _employeeWriteRepository = employeeWriteRepository;
        _createEmployeeRequestValidator = createEmployeeRequestValidator;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to add new employee to database.
    /// </summary>
    /// <param name="request">The request containing employee details to be created.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="CreateEmployeeResponse"/> containing the result of Create operation.</returns>
    public async Task<CreateEmployeeResponse> Handle(AddEmployee request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation("{Handler}.{Method} - Execution started successfully with input : {@Employee}",
            HandlerName, methodName, request.CreateEmployee);

        var createEmployeeRequestValidationResult = 
            await _createEmployeeRequestValidator.ValidateAsync(request.CreateEmployee, cancellationToken);

        if (!createEmployeeRequestValidationResult.IsValid)
            throw new ValidationException(createEmployeeRequestValidationResult.Errors);

        var employee = _mapper.Map<Employee>(request.CreateEmployee);
        var addedEmployee = await _employeeWriteRepository.AddEmployee(employee);

        var response = _mapper.Map<CreateEmployeeResponse>(addedEmployee);

        _logger.LogInformation("{Handler}.{Method} - Execution completed successfully with output : {@Employee}",
            HandlerName, methodName, response);

        return response;
    }

    #endregion
}