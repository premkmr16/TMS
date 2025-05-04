using MediatR;
using TMS.Application.Common.Models;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// The Query to get all the Employees.
/// </summary>
public record GetEmployees(PaginationRequest PaginationRequest) : IRequest<PaginatedResponse<EmployeeResponse>>;