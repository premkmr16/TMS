namespace TMS.Core.Entities;

/// <summary>
/// 
/// </summary>
public class EmployeeRelation : TrackableEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string EmployeeId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Employee Employee { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Relation { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
}