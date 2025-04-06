namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="Category"/> Table in the Database.
/// Defines the classification type for work items.
/// </summary>
public sealed class Category : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Category.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Category Type of WorkItem.
    /// <example>Story</example>
    /// </summary>
    public string Type { get; set; }
}