using System.Text.Json.Serialization;
using TMS.Application.Common.Converters;

namespace TMS.Application.Features.Employees.Contracts.Create;

/// <summary>
/// 
/// </summary>
public class ImportEmployeeRequest
{
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>John Doe</example>
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Contact Number.
    /// <example>8719106611</example>
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Date of Birth.
    /// <example>11-04-2000</example>
    /// </summary>
    [JsonConverter(typeof(GenericDateConvertor<DateTime>))]
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string EmployeeTypeId { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string EmployeeType { get; set; }
    
    /// <summary>
    /// Gets or Sets the start date of employee.
    /// <example>2024-09-25 02:55:03</example>
    /// </summary>
    [JsonConverter(typeof(GenericDateConvertor<DateTimeOffset>))]
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the end date of employee.
    /// <example>2026-10-27 08:27:25</example>
    /// </summary>
    [JsonConverter(typeof(GenericDateConvertor<DateTimeOffset>))]
    public DateTimeOffset EndDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Number for Employee.
    /// <example>89101</example>
    /// </summary>
    public string EmployeeNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee EmailAddress.
    /// <example>johndoe@gmail.com</example>
    /// </summary>
    public string Email { get; set; }
}

/// <summary>
/// 
/// </summary>
public class ImportEmployee
{
    /// <summary>
    /// 
    /// </summary>
    public List<ImportEmployeeRequest> ImportEmployeeRequests { get; set; }
}