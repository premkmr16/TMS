using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// The Query to get Employee by using Unique Identifier of Employee.
/// </summary>
/// <param name="EmployeeId">The unique identifier of employee.</param>
public record GetEmployee(string EmployeeId) :  IRequest<EmployeeResponse>;
