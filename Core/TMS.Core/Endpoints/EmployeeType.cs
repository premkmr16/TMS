namespace TMS.Core.Endpoints;

public static class EmployeeType
{
    public static class EmployeeTypeGroup
    {
        public const string Group = "employee-type";
    }
    
    public const string AddEmployeeType = "add";
    
    public const string GetEmployeeTypes= "get";
    
    public const string GetEmployeeType= "{employeeTypeId}";
}