namespace TMS.Core.Database;

public static class EmployeeQueries
{
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeesQuery =
        """
        SELECT
            employee."Id",
            employee."EmployeeNumber",
            employee."Name",
            employee."Email",
            employee."Phone",
            employeeType."Type",
            employee."IsActive",
            employee."StartDate",
            employee."EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        INNER JOIN "EmployeesTypes" employeesType ON employee."EmployeeTypeId" = employeesType."Id"
        WHERE employee."IsActive" = 1
        """;
    
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeByIdQuery = 
        """
        SELECT
            employee."Id",
            employee."Name",
            employee."Email",
            employee."Phone",
            employeeType."Type",
            employee."IsActive",
            employee."StartDate",
            employee."EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        INNER JOIN "EmployeesTypes" employeesType ON employee."EmployeeTypeId" = employeesType."Id"
        WHERE employee."IsActive" = 1 AND employee."Id" = @EmployeeId
        """;
    
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeByEmailOrEmployeeNumberQuery =
        """
        SELECT
            employee."Id",
            employee."EmployeeNumber",
            employee."Name",
            employee."Email",
            employee."Phone",
            employeeType."Type",
            employee."IsActive",
            employee."StartDate",
            employee."EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        INNER JOIN "EmployeesTypes" employeesType ON employee."EmployeeTypeId" = employeesType."Id"
        WHERE employee."IsActive" = 1 
        AND (employee."Email" = @EmailAddress OR employee."EmployeeNumber" = @EmployeeNumber)
        """;

    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeTypeByIdQuery =
        """
        SELECT
            employeeType."Id",
            employeeType."Type",
            employeeType."CreatedOn",
            employeeType."CreatedBy",
            employeeType."ModifiedOn",
            employeeType."ModifiedBy"
        FROM "EmployeeTypes" employeeType
        WHERE employeeType."Id" = @EmployeeTypeId
        """;
    
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeTypeByNameQuery =
        """
        SELECT
            employeeType."Id",
            employeeType."Type",
            employeeType."CreatedOn",
            employeeType."CreatedBy",
            employeeType."ModifiedOn",
            employeeType."ModifiedBy"
        FROM "EmployeeTypes" employeeType
        WHERE employeeType."Type" = @EmployeeTypeName
        """;
    
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeTypesQuery = 
        """
        SELECT
            employeeType."Id",
            employeeType."Type",
            employeeType."CreatedOn",
            employeeType."CreatedBy",
            employeeType."ModifiedOn",
            employeeType."ModifiedBy"
        FROM "EmployeeTypes" employeeType
        """;
}