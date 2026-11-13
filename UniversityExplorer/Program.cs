using UniversityExplorer.Components;
using UniversityExplorer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ICountryService, CountryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Countries"]!);
    // The maximum time that HttpClient waits for a response from the API.
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<IUniversityService, UniversityService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Universities"]!);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<IStudentApplicationService, StudentApplicationService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:StudentApplications"]!);
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
