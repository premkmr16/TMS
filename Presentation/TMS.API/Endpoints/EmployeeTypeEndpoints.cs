using MediatR;
using TMS.Application.Features.Employees.Commands.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Core.Endpoints;
using static TMS.Core.Endpoints.EmployeeType;

namespace TMS.API.Endpoints;

/// <summary>
/// Provides endpoint mappings for Employee Type related operations.
/// </summary>
public static class EmployeeTypeEndpoints
{
    /// <summary>
    /// Maps the Employee Type endpoints to the application's endpoint routing system.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> instance to configure endpoints on.</param>
    public static void MapToEmployeesEndpoints(this IEndpointRouteBuilder app)
    {
        var employeeTypeGroup = app.MapGroup(EmployeeTypeGroup.Group);

        #region Add

        employeeTypeGroup.MapPost(EmployeeType.AddEmployeeType,
                async (CreateEmployeeTypeRequest employeeTypeRequest,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeTypeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with input EmployeeType : {@EmployeeTypeRequest}",
                        EmployeeTypeGroup.Group, EmployeeType.AddEmployeeType, employeeTypeRequest);

                    var employeeTypeResponse = await mediator.Send(
                        new AddEmployeeType(employeeTypeRequest), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeTypeResponse}",
                        EmployeeTypeGroup.Group, EmployeeType.AddEmployeeType, employeeTypeResponse);

                    return employeeTypeResponse is not null
                        ? Results.Created($"/{EmployeeTypeGroup.Group}/{employeeTypeResponse.Id}",  employeeTypeResponse)
                        : Results.UnprocessableEntity();
                }
            )
            .WithTags("Employee Type")
            .WithName("AddEmployeeTypeEndpoint")
            .WithSummary("Creates new employee type and returns the created record.");
        
        #endregion

        #region GetAll

        employeeTypeGroup.MapGet(EmployeeType.GetEmployeeTypes,
                async (IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeTypeEndpoints));

                    logger.LogInformation("[{Group}].[{API}] - execution started successfully", EmployeeTypeGroup.Group,
                        EmployeeType.GetEmployeeTypes);

                    var employeeTypeResponse =
                        await mediator.Send(new GetEmployeeTypes(), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeTypeResponse}",
                        EmployeeTypeGroup.Group, EmployeeType.AddEmployeeType, employeeTypeResponse);

                    return Results.Ok(employeeTypeResponse);
                }
            )
            .WithTags("Employee Type")
            .WithName("GetEmployeeTypesEndpoint")
            .WithSummary("Returns all employee types.");
        
        #endregion
        
        #region Get

        employeeTypeGroup.MapGet(EmployeeType.GetEmployeeType,
                async (Ulid employeeTypeId,
                    IMediator mediator,
                    ILoggerFactory loggerFactory,
                    CancellationToken cancellationToken) =>
                {
                    var logger = loggerFactory.CreateLogger(nameof(EmployeeTypeEndpoints));

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution started successfully with EmployeeTypeId : {EmployeeTypeId}",
                        EmployeeTypeGroup.Group, EmployeeType.GetEmployeeType, employeeTypeId);

                    var employeeTypeResponse = await mediator.Send(
                        new GetEmployeeType(employeeTypeId), cancellationToken);

                    logger.LogInformation(
                        "[{Group}].[{API}] - execution completed successfully with output : {@EmployeeTypeResponse}",
                        EmployeeTypeGroup.Group, EmployeeType.AddEmployeeType, employeeTypeResponse);

                    return employeeTypeResponse is null
                        ? Results.NotFound()
                        : Results.Ok(employeeTypeResponse);
                }
            )
            .WithTags("Employee Type")
            .WithName("GetEmployeeTypeEndpoint")
            .WithSummary("Returns employee types for the given employee type id.");
        
        #endregion
    }
}