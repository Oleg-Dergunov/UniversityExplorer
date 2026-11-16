namespace UniversityExplorer.Tests.Components;

public class StudentApplicationFormTests : BunitContext
{
    public StudentApplicationFormTests()
    {
        Services.AddSingleton<ICountryService>(new FakeCountryService());
        Services.AddSingleton<IUniversityService>(new FakeUniversityService());
        Services.AddLogging();
    }

    [Fact]
    public void SubmittingEmptyForm_ShowsValidationMessages_AndDoesNotSubmit()
    {
        var submitted = false;

        var cut = Render<StudentApplicationForm>(parameters => parameters
            .Add(p => p.Model, new StudentApplication())
            .Add(p => p.OnValidSubmit, (StudentApplication _) => submitted = true));

        cut.Find("form").Submit();

        Assert.False(submitted);
        Assert.NotEmpty(cut.FindAll(".validation-message"));
    }

    [Fact]
    public void SubmittingValidForm_InvokesOnValidSubmitWithModel()
    {
        StudentApplication? submittedModel = null;
        var model = new StudentApplication
        {
            StudentName = "Test Student",
            Email = "test.student@example.com",
            Country = "Ireland",
            UniversityName = "Trinity College",
            CourseName = "Computing",
            ApplicationDate = new DateTime(2026, 11, 16)
        };

        var cut = Render<StudentApplicationForm>(parameters => parameters
            .Add(p => p.Model, model)
            .Add(p => p.OnValidSubmit, (StudentApplication submitted) => submittedModel = submitted));

        cut.Find("form").Submit();

        Assert.Same(model, submittedModel);
    }

    [Fact]
    public void HidesApplicationDateField_WhenShowApplicationDateIsFalse()
    {
        var cut = Render<StudentApplicationForm>(parameters => parameters
            .Add(p => p.Model, new StudentApplication())
            .Add(p => p.ShowApplicationDate, false));

        Assert.Empty(cut.FindAll("#applicationDate"));
    }
}