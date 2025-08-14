using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// The Query to get EmployeeType by using Unique Identifier of EmployeeType.
/// </summary>
/// <param name="EmployeeTypeId">he unique identifier of employee type.</param>
public record GetEmployeeType(string EmployeeTypeId) : IRequest<EmployeeTypeResponse>;