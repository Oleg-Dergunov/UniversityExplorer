using System.Net;
using System.Net.Http.Json;
using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public class StudentApplicationService : IStudentApplicationService
{
    private const string Endpoint = "studentapplication";

    private readonly HttpClient _httpClient;

    public StudentApplicationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StudentApplication>> GetAllAsync()
    {
        var applications = await _httpClient.GetFromJsonAsync<List<StudentApplication>>(Endpoint);

        return applications?
            .OrderBy(a => a.Id)
            .ToList() ?? new List<StudentApplication>();
    }

    public async Task<StudentApplication?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StudentApplication>($"{Endpoint}/{id}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task CreateAsync(StudentApplication application)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, application);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(int id, StudentApplication application)
    {
        var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", application);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}