namespace TMS.Core.Common;

public static class ExcelConstants
{
    public static class Employee
    {
        public static readonly List<string> EmployeeExcelHeaders =
            ["Employee Number", "Name", "Email", "Phone", "Date of Birth", "Employee Type", "Start Date", "End Date"];
        
        public static readonly string FileName = "Employees.xlsx";
        
        public static readonly string WorkSheetName = "Employees";
    }
    
    public static class Excel
    {
        public const string DateFormat = "dd-MM-yyyy";
    }
}