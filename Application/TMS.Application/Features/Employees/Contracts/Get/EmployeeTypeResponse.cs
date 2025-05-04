using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Contracts.Get;

/// <summary>
/// Represents the response model for EmployeeType Entity.
/// </summary>
public class EmployeeTypeResponse : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for EmployeeType.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Type of Employee.
    /// <example>Intern</example>
    /// </summary>
    public string Type { get; set; }
}