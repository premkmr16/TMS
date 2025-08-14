using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;

namespace TMS.Application.Features.Employees.Helpers;

/// <summary>
/// 
/// </summary>
public static class EmployeeHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="employeeRequests"></param>
    /// <returns></returns>
    public static (List<string> employeeNumbers, List<string> emails) 
        GetEmployeeNumberAndEmail(this List<ImportEmployeeRequest> employeeRequests)
    {
        var employeeNumbers = new HashSet<string>(employeeRequests.Count);
        var emails = new HashSet<string>(employeeRequests.Count);

        foreach (var employee in employeeRequests)
        {
            employeeNumbers.Add(employee.EmployeeNumber);
            emails.Add(employee.Email);
        }

        return (employeeNumbers.ToList(), emails.ToList());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="employeeTypeKey"></param>
    /// <param name="context"></param>
    /// <param name="employeeType"></param>
    /// <returns></returns>
    public static bool IsValidEmployeeType(
        string employeeTypeKey,
        ValidationContext<ImportEmployeeRequest> context,
        string employeeType)
    {
        return context.RootContextData.TryGetValue(employeeTypeKey, out var employeeTypes) &&
               employeeTypes is List<string> validEmployeeTypes &&
               validEmployeeTypes.Contains(employeeType);
    }
}
