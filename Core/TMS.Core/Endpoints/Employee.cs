namespace TMS.Core.Endpoints;

public static class Employee
{
    public static class EmployeeGroup
    {
        public const string Group = "employee";
    }
    
    public const string AddEmployee = "add";
    
    public const string GetEmployee= "id/{employeeId}";
    
    public const string GetEmployees = "get";
    
    public const string GetEmployeeByNumberOrEmail = "contact/{numberOrEmail}";
    
    public const string UpdateEmployee = "update";
    
    public const string ExportEmployee = "Export";
}       