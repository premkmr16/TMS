using Microsoft.AspNetCore.Mvc;

namespace TMS.Application.Common.Models;

/// <summary>
/// 
/// </summary>
public class ErrorResponse : ProblemDetails
{
    /// <summary>
    /// 
    /// </summary>
    public string TraceId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Method { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public IDictionary<string, List<string>> Errors { get; set; }
}