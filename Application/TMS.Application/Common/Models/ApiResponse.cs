namespace TMS.Application.Common.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string TraceId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Method { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public double ExecutionTime { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public T Data { get; set; }
}