namespace TMS.Application.Features.AutoSchemaQL.Contracts;

public class TableInformation
{
    public string Name { get; set; }
    
    public string ColumnName { get; set; }
    
    public string DateType { get; set; }
    
    public string IsNullable { get; set; }
    
    public string Constraint { get; set; }
    
    public string ReferencedTable { get; set; }
    
    public string ReferencedColumn { get; set; }
}