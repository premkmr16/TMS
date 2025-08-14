using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Shouldly;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.ValidationHelpers;
using TMS.Application.Features.Employees.Validators;
using TMS.Application.Tests.Features.EmployeeTests.TestData;
using TMS.Core.Common;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Tests.Features.EmployeeTests.ValidatorTests;

/// <summary>
/// 
/// </summary>
public class CreateEmployeeRequestValidatorTests
{
    #region Fields

    /// <summary>
    /// 
    /// </summary>
    private readonly Mock<IEmployeeValidator> _employeeValidator = new();

    /// <summary>
    /// 
    /// </summary>
    private readonly CreateEmployeeRequestValidator _validator;

    #endregion

    #region Constructor

    /// <summary>
    /// 
    /// </summary>
    public CreateEmployeeRequestValidatorTests()
    {
        _validator = new CreateEmployeeRequestValidator(_employeeValidator.Object);
    }

    #endregion

    #region EmployeeNumber Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeNumber_Is_Empty()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeNumber = string.Empty;

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeNumber)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeNumber_Is_Null()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeNumber = Constants.NullValue;

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
              .WithErrorMessage(string.Format(
                ValidationMessages.CannotBeNullOrEmpty,
                nameof(CreateEmployeeRequest.EmployeeNumber)));
    }

    #endregion

    #region DateOfBirth Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_DateOfBirth_Is_Default()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.DateOfBirth = default;

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.DateOfBirth)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_DateOfBirth_Is_MinValue()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.DateOfBirth = DateTime.MinValue;

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.DateOfBirth)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_DateOfBirth_Is_MaxValue()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.DateOfBirth = DateTime.MaxValue;

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.DateOfBirth)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_DateOfBirth_Does_Not_Match_Age_Criteria()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.DateOfBirth = DateTime.Today.AddYears(-17);

        var result = await _validator.TestValidateAsync(request);

        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(EmployeeValidationMessages.InvalidDateOfBirth);
    }

    #endregion

    #region EmployeeTypeId Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeTypeId_Is_Empty()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeTypeId = string.Empty;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeTypeId)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeTypeId)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeTypeId_Is_WhiteSpace()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeTypeId = " ";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeTypeId)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeTypeId)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeTypeId_Length_Is_Does_Not_Match_Criteria()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeTypeId = "01JXS9B03QPB2AQKVJVMXVA9VW8";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeTypeId)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidStringLength, "Ulid"));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeTypeId_Is_In_Invalid_Format()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.EmployeeTypeId = "01JX&9B_3QPB2AQKVJVMXVA9VW";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeTypeId)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidStringFormat, "Ulid", request.EmployeeTypeId));
    }

    #endregion

    #region  JoiningDate Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_StartDate_Is_MinValue()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.StartDate = DateTimeOffset.MinValue;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.StartDate), request.StartDate));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_StartDate_Is_MaxValue()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.StartDate = DateTimeOffset.MaxValue;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.StartDate), request.StartDate));
    }

    #endregion

    #region PhoneNumber Validation
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Is_Null()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = Constants.NullValue;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
            .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Phone)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Is_Empty()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = string.Empty;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Phone)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Is_WhiteSpace()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = " ";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Phone)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Count_Is_Less_Than_Ten()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = "787898788";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(CreateEmployeeRequest.Phone)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Count_Is_Greater_Than_Ten()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = "78789878867";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(CreateEmployeeRequest.Phone)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_PhoneNumber_Is_InValid_Format()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Phone = "78*89878T@";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(CreateEmployeeRequest.Phone)));
    }

    #endregion

    #region Email Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Email_Is_Null()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Email = Constants.NullValue;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Email)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Email_Is_Empty()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Email = string.Empty;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Email)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Email_Is_WhiteSpace()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Email = " ";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Email)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Email_Is_Invalid()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.Email = "johnDoe&gmail.com";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidEmail, nameof(CreateEmployeeRequest.Email)));
    }

    #endregion
    
    #region ExistingEmployee Validation
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_New_Employee_Number_Already_Exists()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x => 
                x.ValidateCreateEmployeeRequestAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, string, ValidationContext<CreateEmployeeRequest>>((_, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.EmployeeNumber),
                    string.Format(EmployeeValidationMessages.EmployeeNumberInUse, request.EmployeeNumber, "John"));
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
            .WithErrorMessage(string.Format(EmployeeValidationMessages.EmployeeNumberInUse, request.EmployeeNumber, "John"));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_New_Employee_Email_Already_Exists()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x => 
                x.ValidateCreateEmployeeRequestAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, string, ValidationContext<CreateEmployeeRequest>>((_, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.Email),
                    string.Format(EmployeeValidationMessages.EmailInUse, request.Email));
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(EmployeeValidationMessages.EmailInUse, request.Email));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_New_Employee_Number_And_Email_Already_Exists()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x => 
                x.ValidateCreateEmployeeRequestAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, string, ValidationContext<CreateEmployeeRequest>>((_, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.EmployeeNumber),
                    string.Format(EmployeeValidationMessages.EmployeeNumberInUse, request.EmployeeNumber, "John"));
                
                context.AddFailure(nameof(CreateEmployeeRequest.Email),
                    string.Format(EmployeeValidationMessages.EmailInUse, request.Email));
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(2);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(string.Format(EmployeeValidationMessages.EmailInUse, request.Email));
        result.ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
            .WithErrorMessage(string.Format(
                EmployeeValidationMessages.EmployeeNumberInUse, 
                request.EmployeeNumber,
                "John"));
    }
    
    #endregion

    #region Employee Joining Date Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Contractor_Or_Intern_Date_Is_Invalid()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x =>
                x.ValidateEmployeeEndDate(
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, DateTimeOffset, DateTimeOffset,
                ValidationContext<CreateEmployeeRequest>>((_, _, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                    string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.EndDate),
                        request.EndDate));
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
              .WithErrorMessage(string.Format(ValidationMessages.InvalidDateFormat, 
                  nameof(CreateEmployeeRequest.EndDate), 
                  request.EndDate));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Contractor_Or_Intern_EndDate_Is_LessThan_StartDate()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x =>
                x.ValidateEmployeeEndDate(
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, DateTimeOffset, DateTimeOffset,
                ValidationContext<CreateEmployeeRequest>>((_, _, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                    string.Format(ValidationMessages.EndDateTooSoon, nameof(CreateEmployeeRequest.EndDate),
                        request.StartDate.AddMonths(1).Date.ToShortDateString()));
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
              .WithErrorMessage(string.Format(ValidationMessages.EndDateTooSoon, nameof(CreateEmployeeRequest.EndDate),
                request.StartDate.AddMonths(1).Date.ToShortDateString()));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EndDate_Is_Provided_For_EmployeeTypes_Other_Than_Contractor_Or_Intern()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();

        _employeeValidator.Setup(x =>
                x.ValidateEmployeeEndDate(
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<ValidationContext<CreateEmployeeRequest>>()))
            .Callback<string, DateTimeOffset, DateTimeOffset,
                ValidationContext<CreateEmployeeRequest>>((_, _, _, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeRequest.EndDate),
                    EmployeeValidationMessages.EndDateShouldBeNull);
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
              .WithErrorMessage(EmployeeValidationMessages.EndDateShouldBeNull);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task should_Not_Validate_EndDate_If_EmployeeData_Is_verified_Already()
    {
        var request = EmployeeTestData.GetCreateEmployeeRequestFaker().Generate();
        request.IsVerified = true;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(0);
        result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
    }

    #endregion
}
