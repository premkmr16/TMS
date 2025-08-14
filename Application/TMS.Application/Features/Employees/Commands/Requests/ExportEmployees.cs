using MediatR;

namespace TMS.Application.Features.Employees.Commands.Requests;

/// <summary>
/// The Command to get all the employee information and store it in Excel.
/// </summary>
public record ExportEmployees() : IRequest<byte[]>;