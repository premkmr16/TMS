using MediatR;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// 
/// </summary>
/// <param name="EmployeeTypeId"></param>
public record GetEmployeeType(string EmployeeTypeId) : IRequest<EmployeeTypeResponse>;