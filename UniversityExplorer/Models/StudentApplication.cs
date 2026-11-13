using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityExplorer.Models;

public class StudentApplication
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Student name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Student name must be between 2 and 100 characters.")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "University name is required.")]
    [JsonPropertyName("university")]
    public string UniversityName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course name is required.")]
    [JsonPropertyName("course")]
    public string CourseName { get; set; } = string.Empty;

    // The API does not yet return this field, so it is nullable.
    [Required(ErrorMessage = "Application date is required.")]
    [DataType(DataType.Date)]
    public DateTime? ApplicationDate { get; set; }

    // It’s in the API, though not mentioned in the task requirements.
    [StringLength(1000, ErrorMessage = "Personal statement cannot exceed 1000 characters.")]
    public string? PersonalStatement { get; set; }
}