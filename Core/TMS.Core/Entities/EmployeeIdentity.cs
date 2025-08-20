namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeIdentity"/> Table in the Database.
/// Defines the employee identity details such as personal information, contact details, and identification numbers.
/// </summary>
public sealed class EmployeeIdentity : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee Identity.
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
    /// Gets or sets the Employee EmailAddress.
    /// <example>johndoe@gmail.com</example>
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Emergency Contact Name.
    /// <example>Jane</example>
    /// </summary>
    public string EmergencyContactName { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Emergency Contact Number.
    /// <example>8915729105</example>
    /// </summary>
    public string EmergencyContactNumber { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Aadhaar Number.
    /// <example>123456789012</example>
    /// </summary>
    public string AadharNumber { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee PAN Number.
    /// <example>DACBE98164F</example>
    /// </summary>
    public string PanNumber { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Passport Number.
    /// <example>M1234567</example>
    /// </summary>
    public string PassportNumber { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Voter ID.
    /// <example>XYZ123456789</example>
    /// </summary>
    public string VoterId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Blood Group.
    /// <example>O+</example>
    /// </summary>
    public string BloodGroup { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Gender
    /// <example>Male</example>
    /// </summary>
    public string Gender { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Current Address.
    /// <example>11/12,grd Flr, Candy Castle Bldg, Colaba</example>
    /// </summary>
    public string CurrentAddress { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Permanent Address.
    /// <example>11/12,grd Flr, Candy Castle Bldg, Colaba</example>
    /// </summary>
    public string PermanentAddress { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee City.
    /// <example>Madurai</example>
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee State.
    /// <example>TamilNadu</example>
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Country.
    /// <example>India</example>
    /// </summary>
    public string Country { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Postal Code.
    /// <example>600083</example>
    /// </summary>
    public string PostalCode { get; set; }
}