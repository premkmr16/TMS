using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
///
/// </summary>
public record GetEmployeeTypes() : IRequest<List<EmployeeTypeResponse>>;
