using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// The Query to get Employee by Employee Number or EmailAddress.
/// </summary>
/// <param name="EmployeeNumber">The unique Number assigned to employee during creation.</param>
/// <param name="EmailAddress">The unique Email assigned to employee during creation.</param>
public record GetEmployeeByNumberOrEmail(string EmployeeNumber = null, string EmailAddress = null)
    : IRequest<EmployeeResponse>;