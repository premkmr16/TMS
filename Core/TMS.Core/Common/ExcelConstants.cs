namespace TMS.Core.Common;

public static class ExcelConstants
{
    public static class Employee
    {
        public static readonly List<string> EmployeeExcelHeaders = 
            ["Employee Number", "Name", "Email", "Phone", "Date of Birth", "Employee Type", "Start Date", "End Date"];

        public const string FileName = "Employees.xlsx";

        public const string WorkSheetName = "Employees";
    }
    
    public static class Excel
    {
        public const string DateFormat = "dd-MM-yyyy";

        public const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}