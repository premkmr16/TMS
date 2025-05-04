namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="ProjectRole"/> Table in the Database.
/// Defines the different Types of Roles in the Project.
/// </summary>
public sealed class ProjectRole : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for ProjectRole.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the ProjectRole Name.
    /// <example>Senior Engineer</example>
    /// </summary>
    public string Name { get; set; }
}