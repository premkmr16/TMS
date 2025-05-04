namespace TMS.Application.Common.Models;

/// <summary>
/// 
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// 
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string SortField { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string SortDirection { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, List<string>> Filters { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool FetchWithPagination { get; set; }
}