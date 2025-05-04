using MediatR;

namespace TMS.Application.Features.Employees.Queries.Requests;

public record ExportEmployees() : IRequest<byte[]>;