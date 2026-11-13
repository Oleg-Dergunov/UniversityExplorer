using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public interface IStudentApplicationService
{
    Task<List<StudentApplication>> GetAllAsync();
    Task<StudentApplication?> GetByIdAsync(int id);
    Task CreateAsync(StudentApplication application);
    Task UpdateAsync(int id, StudentApplication application);
}