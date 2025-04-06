using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// The Query to get all the Employees.
/// </summary>
public record GetEmployees() : IRequest<List<EmployeeResponse>>;