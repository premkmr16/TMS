namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="Priority"/> Table in the Database.
/// Defines the different Priority Types for WorkItems.
/// </summary>
public sealed class Priority : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Priority.
    /// <example>1JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Priority Type of WorkItem.
    /// <example>High</example>
    /// </summary>
    public string Type { get; set; }
}