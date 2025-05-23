namespace TMS.Application.Features.Employees.Contracts.Update;

/// <summary>
/// Represents a request model to update an existing Employee details.
/// </summary>
public class UpdateEmployeeRequest : BaseEmployeeContract
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
}