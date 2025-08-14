namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeRole"/> Table in the Database.
/// Defines the different employee roles within the organization.
/// </summary>
public sealed class EmployeeRole : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for EmployeeRole.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Role of Employee.
    /// <example>Senior Software Engineer</example>
    /// </summary>
    public string Name { get; set; }
}