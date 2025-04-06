namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeType"/> Table in the Database.
/// Defines the different employee types within the organization.
/// </summary>
public sealed class EmployeeType : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for EmployeeType.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Type of Employee.
    /// <example>Intern</example>
    /// </summary>
    public string Type { get; set; }
}