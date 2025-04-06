using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Contracts.Create;

/// <summary>
/// Represents a response model for newly created Employee.
/// </summary>
public class CreateEmployeeResponse : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>Contractor</example>
    /// </summary>
    public Ulid EmployeeType { get; set; }
    
    /// <summary>
    /// Gets or Sets the employee is active or inactive.
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
}