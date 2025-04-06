using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Commands.Handlers;

/// <summary>
/// Handles the process of adding a new employee type.
/// </summary>
public class AddEmployeeTypeHandler : IRequestHandler<AddEmployeeType, EmployeeTypeResponse>
{
    #region Fields
    
    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(AddEmployeeTypeHandler);
    
    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeWriteRepository  _employeeWriteRepository;
    
    /// <summary>
    /// Defines the validator for validating <see cref="CreateEmployeeTypeRequest"/>.
    /// </summary>
    private readonly IValidator<CreateEmployeeTypeRequest> _createEmployeeTypeRequestValidator;
    
    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="AddEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<AddEmployeeTypeHandler> _logger;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="AddEmployeeTypeHandler"/>
    /// </summary>
    /// <param name="employeeWriteRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="createEmployeeTypeRequestValidator">Defines the validator of <see cref="CreateEmployeeTypeRequest"/>.</param>
    /// <param name="mapper">Defines the Mapper of Employee <see cref="IMapper"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="AddEmployeeTypeHandler"/>.</param>
    public AddEmployeeTypeHandler(
        IEmployeeWriteRepository employeeWriteRepository,
        IValidator<CreateEmployeeTypeRequest> createEmployeeTypeRequestValidator,
        IMapper mapper,
        ILogger<AddEmployeeTypeHandler> logger)
    {
        _employeeWriteRepository = employeeWriteRepository;
        _createEmployeeTypeRequestValidator = createEmployeeTypeRequestValidator;
        _mapper = mapper;
        _logger = logger;
    }
    
    #endregion
    
    #region Handler
    
    /// <summary>
    /// The Handler method implements the functionality to add new employee type to database.
    /// </summary>
    /// <param name="request">The request containing employee type to be created.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="EmployeeTypeResponse"/> containing the result of Create operation.</returns>
    public async Task<EmployeeTypeResponse> Handle(AddEmployeeType request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully with input : {@Employee}",
            HandlerName, methodName, request.EmployeeTypeRequest);

        var createEmployeeTypeRequestValidationResult =
            await _createEmployeeTypeRequestValidator.ValidateAsync(request.EmployeeTypeRequest, cancellationToken);

        if (!createEmployeeTypeRequestValidationResult.IsValid)
            throw new ValidationException(createEmployeeTypeRequestValidationResult.Errors);
        
        var employeeType = _mapper.Map<EmployeeType>(request.EmployeeTypeRequest);
        var addedEmployeeType = await _employeeWriteRepository.AddEmployeeType(employeeType);
        
        var response = _mapper.Map<EmployeeTypeResponse>(addedEmployeeType);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution completed successfully with output : {@EmployeeType}",
            HandlerName, methodName, response);
        
        return response;
    }
    
    #endregion
}