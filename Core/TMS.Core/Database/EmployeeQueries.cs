namespace TMS.Core.Database;

public static class EmployeeQueries
{
    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeesQuery =
        "SELECT * FROM GetEmployees(@SortField, @SortDirection, @PageSize, @PageNumber, @Filters::JSONB, @FetchWithPagination)";

    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeByIdWithEmployeeTypeNameQuery =
        """
        SELECT
            employee."Id",
            employee."EmployeeNumber",
            employee."Name",
            employee."Email",
            employee."Phone",
            employee."DateOfBirth",
            employeeType."Type",
            employee."IsActive",
            employee."StartDate",
            CASE 
               WHEN employee."EndDate" = '-infinity' THEN NULL
               ELSE employee."EndDate"
            END AS "EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        INNER JOIN "EmployeeTypes" employeeType ON employee."EmployeeTypeId" = employeeType."Id"
        WHERE employee."IsActive" = true AND employee."Id" = @EmployeeId
        """;

    /// <summary>
    /// 
    /// </summary>
    public const string GetEmployeeByIdWithoutEmployeeTypeNameQuery =
        """
        SELECT
            employee."Id",
            employee."EmployeeNumber",
            employee."Name",
            employee."Email",
            employee."Phone",
            employee."DateOfBirth",
            employee."EmployeeTypeId",
            employee."IsActive",
            employee."StartDate",
            CASE 
               WHEN employee."EndDate" = '-infinity' THEN NULL
               ELSE employee."EndDate"
            END AS "EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        WHERE employee."IsActive" = true AND employee."Id" = @EmployeeId
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
            employee."DateOfBirth",
            employeeType."Type",
            employee."IsActive",
            employee."StartDate",
            CASE 
               WHEN employee."EndDate" = '-infinity' THEN NULL
               ELSE employee."EndDate"
            END AS "EndDate",
            employee."CreatedOn",
            employee."CreatedBy",
            employee."ModifiedOn",
            employee."ModifiedBy"
        FROM "Employees" employee
        INNER JOIN "EmployeeTypes" employeeType ON employee."EmployeeTypeId" = employeeType."Id"
        WHERE employee."IsActive" = true
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

    /// <summary>
    /// 
    /// </summary>
    public const string GetUniqueEmployeeIdentifiers = 
        """
        SELECT 
            employee."EmployeeNumber",
            employee."Email"
        FROM "Employees" employee
        WHERE employee."EmployeeNumber" IN (@EmployeeNumbers) OR 
              employee."Email" IN (@Emails)
        """;
}