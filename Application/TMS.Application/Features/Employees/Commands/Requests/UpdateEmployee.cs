using MediatR;
using TMS.Application.Features.Employees.Contracts.Update;

namespace TMS.Application.Features.Employees.Commands.Requests;

/// <summary>
/// The command to update a existing employee.
/// </summary>
/// <param name="Employee">The details of updated employee.</param>
public record UpdateEmployee(UpdateEmployeeRequest Employee) : IRequest<UpdateEmployeeResponse>;