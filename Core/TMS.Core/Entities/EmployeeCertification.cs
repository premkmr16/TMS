namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeCertification"/> Table in the Database.
/// Defines the certification information of employee.
/// </summary>
public sealed class EmployeeCertification : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee certification Information.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Employee.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string EmployeeId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Details.
    /// </summary>
    public Employee Employee { get; set; }
    
    /// <summary>
    /// Gets or Sets the Certification Name of Employee.
    /// <example>AZ204</example>
    /// </summary>
    public string CertificationName { get; set; }
    
    /// <summary>
    /// Gets or Sets the Issued Organization of Employee's Certification.
    /// <example>Microsoft</example>
    /// </summary>
    public string IssuingOrganization { get; set; }
    
    /// <summary>
    /// Gets or Sets the Certification Date Issued By Organization.
    /// <example>2023-10-01</example>
    /// </summary>
    public DateTimeOffset IssuedDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Expiration Date of Employee's Certification.
    /// <example>2025-10-01</example>
    /// </summary>
    public DateTimeOffset ExpirationDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier for Certification.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string CertificationId{ get; set; }
    
    /// <summary>
    /// Gets or Sets the URL for Certification Details.
    /// <example>https://www.example.com/certification/01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string CertificationUrl { get; set; }
    
    /// <summary>
    /// Gets or Sets the Status of Employee's Certification.
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
}