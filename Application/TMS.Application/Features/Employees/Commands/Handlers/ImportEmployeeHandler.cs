using System.Text.Json;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Entities;
using static TMS.Core.Common.Employee;
using static TMS.Core.Common.EmployeeType;

namespace TMS.Application.Features.Employees.Commands.Handlers;

/// <summary>
/// Handles the process to import all the employee information to database.
/// </summary>
public class ImportEmployeeHandler : IRequestHandler<ImportEmployees>
{
    #region Fields

    /// <summary>
    /// The name of the Handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(ImportEmployeeHandler);

    /// <summary>
    /// Defines the Mediator instance used to send commands and queries.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeWriteRepository _employeeWriteRepository;

    /// <summary>
    /// Defines the Employee Repository for performing employee related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Defines the validator for validating <see cref="ImportEmployee"/>.
    /// </summary>
    private readonly IValidator<ImportEmployee> _importEmployeeValidator;

    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Logger instance for capturing <see cref="ImportEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<ImportEmployeeHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="ImportEmployeeHandler"/>.
    /// </summary>
    /// <param name="mediator">Defines the Mediator <see cref="IMediator"/>.</param>
    /// <param name="mapper">Defines the Mapper of Employee <see cref="IMapper"/>.</param>
    /// <param name="employeeWriteRepository">Defines the Employee Repository <see cref="IEmployeeWriteRepository"/>.</param>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="importEmployeeValidator">Defines the validator of <see cref="ImportEmployee"/>.</param>
    /// <param name="logger"></param>
    public ImportEmployeeHandler(
        IMediator mediator,
        IMapper mapper,
        IEmployeeWriteRepository employeeWriteRepository,
        IEmployeeReadRepository employeeReadRepository,
        IValidator<ImportEmployee> importEmployeeValidator,
        ILogger<ImportEmployeeHandler> logger)
    {
        _mediator = mediator;
        _employeeWriteRepository = employeeWriteRepository;
        _employeeReadRepository = employeeReadRepository;
        _importEmployeeValidator = importEmployeeValidator;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Handlers

    /// <summary>
    /// The Handler method implements the functionality to import all the employee information to database.
    /// </summary>
    /// <param name="request">The request containing the Excel file which contains the employee information.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Saves the employee information in Excel to database.</returns>
    public async Task Handle(ImportEmployees request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully with file name : {FileName}",
            HandlerName, methodName, request.File.FileName);

        var excelData = await _mediator.Send(new ExtractInformation(request.File), cancellationToken);
        var importRequest = new ImportEmployee { ImportEmployeeRequests = JsonSerializer.Deserialize<List<ImportEmployeeRequest>>(excelData) };

        var employeeTypes = await _employeeReadRepository.GetEmployeeTypes();
        
        var employeeTypeNames = employeeTypes.Select(x => x.Type).ToList();
        var contractOrInternType = employeeTypes.Where(x => x.Type is Contractor or Intern).Select(x => x.Type).ToList();

        var importEmployeeRequestValidationResult = await _importEmployeeValidator.ValidateAsync(
            new ValidationContext<ImportEmployee>(importRequest)
            {
                RootContextData =
                {
                    [EmployeeTypes] = employeeTypeNames,
                    [ContractOrIntern] = contractOrInternType
                }
            }, cancellationToken);

        if (!importEmployeeRequestValidationResult.IsValid)
            throw new ValidationException(importEmployeeRequestValidationResult.Errors);
        
        importRequest.ImportEmployeeRequests.ForEach(employee => 
            employee.EmployeeTypeId = employeeTypes.Find(x => x.Type == employee.EmployeeType).Id);

        var employees = _mapper.Map<List<Employee>>(importRequest.ImportEmployeeRequests);

        await _employeeWriteRepository.AddEmployees(employees);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully", HandlerName, methodName);
    }

    #endregion
}