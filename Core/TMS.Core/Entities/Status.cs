namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="Status"/> Table in the Database.
/// Defines the different Types of Status for WorkItem.
/// </summary>
public sealed class Status : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Status.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Status
    /// <example>In Development</example>.
    /// </summary>
    public string Type { get; set; }
}