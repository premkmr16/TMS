using MediatR;
using TMS.Application.Common.Models;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Core.Common;
using TMS.Core.Endpoints;
using UpdateEmployee = TMS.Application.Features.Employees.Commands.Requests.UpdateEmployee;

namespace TMS.API.Endpoints;

/// <summary>
/// Provides endpoint mappings for Employee related operations.
/// </summary>
public static class EmployeeEndpoints
{
    /// <summary>
    /// Maps the Employee endpoints to the application's endpoint routing system.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> instance to configure endpoints on.</param>
    public static void MapToEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var employeeGroup = app.MapGroup(Employee.EmployeeGroup.Group);

        #region Add

        employeeGroup.MapPost(Employee.AddEmployee,
                async (CreateEmployeeRequest employeeRequest,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with input Employee : {@EmployeeRequest}",
                        Employee.EmployeeGroup.Group, Employee.AddEmployee, employeeRequest);

                    var employeeResponse = await mediator.Send(
                        new AddEmployee(employeeRequest), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeResponse}",
                        Employee.EmployeeGroup.Group, Employee.AddEmployee, employeeResponse);

                    return employeeResponse is not null
                        ? Results.Created($"/{Employee.EmployeeGroup.Group}/{employeeResponse.Id}", employeeResponse)
                        : Results.NoContent();
                }
            )
            .WithTags("Employee")
            .WithName("AddEmployeeEndpoint")
            .WithSummary("Creates new employee and returns the created record.");

        #endregion

        #region get

        employeeGroup.MapPost(Employee.GetEmployees,
                async (PaginationRequest request,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation("[{Group}].[{API}] - execution started successfully",
                        Employee.EmployeeGroup.Group,
                        Employee.GetEmployees);

                    var employeeResponse =
                        await mediator.Send(new GetEmployees(request), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeResponse}",
                        Employee.EmployeeGroup.Group, Employee.GetEmployees, employeeResponse);

                    return Results.Ok(employeeResponse);
                }
            )
            .WithTags("Employee")
            .WithName("GetEmployeesEndpoint")
            .WithSummary("Returns all employee records.");

        #endregion

        #region GetById

        employeeGroup.MapGet(Employee.GetEmployee,
                async (string employeeId,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with EmployeeId : {EmployeeId}",
                        Employee.EmployeeGroup.Group, Employee.GetEmployee, employeeId);

                    var employeeResponse = await mediator.Send(
                        new GetEmployee(employeeId), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeResponse}",
                        Employee.EmployeeGroup.Group, Employee.GetEmployee, employeeResponse);

                    return employeeResponse is null
                        ? Results.NotFound()
                        : Results.Ok(employeeResponse);
                }
            )
            .WithTags("Employee")
            .WithName("GetEmployeeEndpoint")
            .WithSummary("Returns employee for the given employee id.");

        #endregion

        #region GetEmployeeByNumberOrEmail

        employeeGroup.MapGet(Employee.GetEmployeeByNumberOrEmail,
                async (IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken,
                    string? employeeNumber = null,
                    string? email = null) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with EmployeeNumber : {EmployeeNumber} and Email : {Email}",
                        Employee.EmployeeGroup.Group, Employee.GetEmployeeByNumberOrEmail, employeeNumber, email);

                    var employeeResponse = await mediator.Send(
                        new GetEmployeeByNumberOrEmail(employeeNumber, email), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeResponse}",
                        Employee.EmployeeGroup.Group, Employee.GetEmployeeByNumberOrEmail, employeeResponse);

                    return employeeResponse is null
                        ? Results.NotFound()
                        : Results.Ok(employeeResponse);
                }
            )
            .WithTags("Employee")
            .WithName("GetEmployeeByNumberOrEmailEndpoint")
            .WithSummary("Returns employee for the given employee number or email.");

        #endregion

        #region Update

        employeeGroup.MapPut(Employee.UpdateEmployee,
                async (UpdateEmployeeRequest employeeRequest,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with input Employee : {@EmployeeRequest}",
                        Employee.EmployeeGroup.Group, Employee.UpdateEmployee, employeeRequest);

                    var employeeResponse = await mediator.Send(
                        new UpdateEmployee(employeeRequest), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeResponse}",
                        Employee.EmployeeGroup.Group, Employee.UpdateEmployee, employeeResponse);

                    return employeeResponse is not null
                        ? Results.Ok(employeeResponse)
                        : Results.UnprocessableEntity();
                }
            )
            .WithTags("Employee")
            .WithName("UpdateEmployeeEndpoint")
            .WithSummary("updates employees info except employee number and email.");

        #endregion

        #region Export

        employeeGroup.MapPost(Employee.ExportEmployee,
                async (IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully", Employee.EmployeeGroup.Group,
                        Employee.ExportEmployee);

                    var bytes = await mediator.Send(new ExportEmployees(), cancellationToken);

                    logger.LogInformation("[{Group}].[{API}] - execution completed successfully",
                        Employee.EmployeeGroup.Group, Employee.ExportEmployee);

                    return Results.File(bytes, ExcelConstants.Excel.ContentType, ExcelConstants.Employee.FileName);
                }
            )
            .WithTags("Employee")
            .WithName("ExportEmployeeEndpoint")
            .WithSummary("Exports all employee information to Excel.");

        #endregion
        
        #region Import

        employeeGroup.MapPost(Employee.ImportEmployee,
                async (IFormFile file,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully", Employee.EmployeeGroup.Group,
                        Employee.ImportEmployee);

                    await mediator.Send(new ImportEmployees(file), cancellationToken);

                    logger.LogInformation("[{Group}].[{API}] - execution completed successfully",
                        Employee.EmployeeGroup.Group, Employee.ImportEmployee);

                    return Results.Ok();
                }
            )
            .WithTags("Employee")
            .WithName("ImportEmployeeEndpoint")
            .WithSummary("Import all employee information to database.")
            .DisableAntiforgery();

        #endregion
    }
}