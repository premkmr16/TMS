namespace TMS.Core.Entities;

/// <summary>
/// Defines the <see cref="TrackableEntity"/> Abstract Class for Tracking Audit Information of Records.
/// </summary>
public abstract class TrackableEntity : ITrackableEntity
{
    /// <inheritdoc cref="ITrackableEntity.CreatedOn"/>
    public DateTimeOffset CreatedOn { get; set; }
    
    /// <inheritdoc cref="ITrackableEntity.CreatedBy"/>
    public string CreatedBy { get; set; }
    
    /// <inheritdoc cref="ITrackableEntity.ModifiedOn"/>
    public DateTimeOffset ModifiedOn { get; set; }
    
    /// <inheritdoc cref="ITrackableEntity.ModifiedBy"/>
    public string ModifiedBy { get; set; }
}