namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeEducation"/> Table in the Database.
/// Defines the educational information of employee.
/// </summary>
public sealed class EmployeeEducation : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee Education Information.
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
    /// Gets or Sets the Educational Level of Employee.
    /// <example>Graduate</example>
    /// </summary>
    public string EducationalLevel { get; set; }
    
    /// <summary>
    /// Gets or Sets the Qualification of Employee.
    /// <example>BTech</example>
    /// </summary>
    public string Qualification { get; set; }
    
    /// <summary>
    /// Gets or Sets the Institution Name of Employee.
    /// <example>xyz College of Engineering</example>
    /// </summary>
    public string Institution { get; set; }
    
    /// <summary>
    /// Gets or Sets the Board or University of Employee's Education.
    /// <example>XYZ University</example>
    /// </summary>
    public string BoardOrUniversity { get; set; }
    
    /// <summary>
    /// Gets or Sets the Year of Passing for Employee's Education.
    /// <example>2023</example>
    /// </summary>
    public string YearOfPassing { get; set; }
    
    /// <summary>
    /// Gets or Sets the Percentage or CGPA of Employee's Education.
    /// <example>8.5</example>
    /// </summary>
    public decimal PercentageOrCgpa { get; set; }
    
    /// <summary>
    /// Gets or Sets the Specialization of Employee's Education.
    /// <example>Computer Science</example>
    /// </summary>
    public string Specialization { get; set; }
    
    /// <summary>
    /// Gets or Sets the City of Employee's Education Institution.
    /// <example>Bangalore</example>
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Gets or Sets the State of Employee's Education Institution.
    /// <example>Karnataka</example>
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// Gets or Sets the Country of Employee's Education Institution.
    /// <example>India</example>
    /// </summary>
    public string Country { get; set; }
    
    /// <summary>
    /// Gets or Sets the Postal Code of Employee's Education Institution.
    /// <example>560001</example>
    /// </summary>
    public string PostalCode { get; set; }
}