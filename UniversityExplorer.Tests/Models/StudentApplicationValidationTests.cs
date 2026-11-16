using System.ComponentModel.DataAnnotations;

namespace UniversityExplorer.Tests.Models;

public class StudentApplicationValidationTests
{
    [Fact]
    public void ValidApplication_HasNoValidationErrors()
    {
        var results = Validate(CreateValidApplication());

        Assert.Empty(results);
    }

    [Fact]
    public void EmptyApplication_RequiresAllMandatoryFields()
    {
        var results = Validate(new StudentApplication());

        var invalidFields = results
            .SelectMany(r => r.MemberNames)
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        var expectedFields = new[]
        {
            nameof(StudentApplication.ApplicationDate),
            nameof(StudentApplication.Country),
            nameof(StudentApplication.CourseName),
            nameof(StudentApplication.Email),
            nameof(StudentApplication.StudentName),
            nameof(StudentApplication.UniversityName)
        };

        Assert.Equal(expectedFields, invalidFields);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("not-an-email")]
    public void InvalidEmail_FailsValidation(string email)
    {
        var application = CreateValidApplication();
        application.Email = email;

        var results = Validate(application);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(StudentApplication.Email)));
    }

    private static StudentApplication CreateValidApplication() => new()
    {
        StudentName = "Test Student",
        Email = "test.student@example.com",
        Country = "Ireland",
        UniversityName = "Trinity College",
        CourseName = "Computing",
        ApplicationDate = new DateTime(2026, 11, 16)
    };

    private static List<ValidationResult> Validate(StudentApplication application)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(application, new ValidationContext(application), results, validateAllProperties: true);
        return results;
    }
}