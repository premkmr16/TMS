namespace TMS.Application.Common.Models;

/// <summary>
/// 
/// </summary>
public class ExportExcelRequest
{
    /// <summary>
    /// 
    /// </summary>
    public List<string> Headers { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public List<List<string>> Data { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string WorkSheetName { get; set; }
}