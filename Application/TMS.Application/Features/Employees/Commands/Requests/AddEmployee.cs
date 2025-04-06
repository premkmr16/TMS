using MediatR;
using TMS.Application.Features.Employees.Contracts.Create;

namespace TMS.Application.Features.Employees.Commands.Requests;

/// <summary>
/// The command to add a new employee.
/// </summary>
/// <param name="CreateEmployee">The details of new employee.</param>
public record AddEmployee(CreateEmployeeRequest CreateEmployee) : IRequest<CreateEmployeeResponse>;