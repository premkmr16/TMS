namespace TMS.Core.Entities;

/// <summary>
/// Defines the <see cref="ITrackableEntity"/> interface for Tracking Audit Information of Records.
/// </summary>
public interface ITrackableEntity
{
    /// <summary>
    /// Gets or Sets the Created Date of Record.
    /// <example>2024-01-01 08:55:03</example>
    /// </summary>
    public DateTimeOffset CreatedOn { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee UserID for the Record Created.
    /// <example>JohnDoe</example>
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Gets or Sets the Modified Date of Record.
    /// <example>2024-08-16 08:43:16</example>
    /// </summary>
    public DateTimeOffset ModifiedOn { get; set; }
    
    /// <summary>
    ///  Gets or Sets the Employee UserID for the Record Modified.
    /// <example>JaneDoe</example>
    /// </summary>
    public string ModifiedBy { get; set; }
}