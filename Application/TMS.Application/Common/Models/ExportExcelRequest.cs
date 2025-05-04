namespace TMS.Application.Common.Models;

public class ExportExcelRequest
{
    public List<string> Headers { get; set; }
    
    public List<List<string>> Data { get; set; }
    
    public string FileName { get; set; }
    
    public string WorkSheetName { get; set; }
}