using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;
using TMS.Application.Common.Helpers;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using static TMS.Core.Common.ExcelConstants;

namespace TMS.Application.Features.Employees.Commands.Handlers;

/// <summary>
/// Handles the process to export all the employee information to excel
/// </summary>
public class ExportEmployeeHandler : IRequestHandler<ExportEmployees, byte[]>
{
    #region Fields
    
    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(ExportEmployeeHandler);
    
    /// <summary>
    /// Defines the Mediator instance used to send commands and queries.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Defines the Helper service for Excel related operations.
    /// </summary>
    private readonly IExcelHelper _excelHelper;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="ExportEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<ExportEmployeeHandler> _logger;
    
    #endregion
    
    #region Contructors

    /// <summary>
    /// Initializes the new instance of <see cref="ExportEmployeeHandler"/>
    /// </summary>
    /// <param name="mediator">Defines the Mediator <see cref="IMediator"/></param>
    /// <param name="excelHelper">Defines the Excel helper <see cref="IExcelHelper"/></param>
    /// <param name="logger">Defines the logger instance of <see cref="ExportEmployeeHandler"/></param>
    public ExportEmployeeHandler(
        IMediator mediator,
        IExcelHelper excelHelper,
        ILogger<ExportEmployeeHandler> logger)
    {
        _mediator = mediator;
        _excelHelper = excelHelper;
        _logger = logger;
    }
    
    #endregion
    
    #region Handlers

    /// <summary>
    /// The Handler method implements the functionality to export all the employee information to excel.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">A token to observe for cancellation requests</param>
    /// <returns>The employee byte array that is to be stored in Excel file</returns>
    public async Task<byte[]> Handle(ExportEmployees request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully", HandlerName, methodName);
        
        var paginationRequest = _excelHelper.BuildDefaultExcelPaginationRequest(nameof(EmployeeResponse.Id));
       
        var employeeResponse = await _mediator.Send(new GetEmployees(paginationRequest), cancellationToken);

        var exportExcelRequest = _excelHelper.BuildExcelRequest(
            Employee.EmployeeExcelHeaders, 
            employee => [
                employee.EmployeeNumber,
                employee.Name,
                employee.Email,
                employee.Phone,
                employee.DateOfBirth.Date.ToString(Excel.DateFormat),
                employee.Type,
                employee.StartDate.Date.ToString(Excel.DateFormat),
                employee.EndDate is not null
                    ? employee.EndDate.Value.Date.ToString(Excel.DateFormat)
                    : string.Empty
            ], 
            employeeResponse.Data, 
            Employee.WorkSheetName, 
            Employee.FileName);

        var excelBytes = await _mediator.Send(new ExportInformation(exportExcelRequest), cancellationToken);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully", HandlerName, methodName);     
        
        return excelBytes;
    }
    
    #endregion
}