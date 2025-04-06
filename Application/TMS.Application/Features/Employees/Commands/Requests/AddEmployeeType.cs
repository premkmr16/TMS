using MediatR;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Get;

namespace TMS.Application.Features.Employees.Commands.Requests;

/// <summary>
/// The command to add a new employee type.
/// </summary>
/// <param name="EmployeeTypeRequest">The details of the employee type</param>
public record AddEmployeeType(CreateEmployeeTypeRequest EmployeeTypeRequest) : IRequest<EmployeeTypeResponse>;
