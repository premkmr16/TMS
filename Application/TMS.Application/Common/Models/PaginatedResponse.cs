namespace TMS.Application.Common.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedResponse<T>
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
    public List<T> Data { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Total { get; set; }
}