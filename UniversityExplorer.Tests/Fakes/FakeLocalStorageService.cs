using System.Text.Json;

namespace UniversityExplorer.Tests.Fakes;

public class FakeLocalStorageService : ILocalStorageService
{
    public Dictionary<string, string> Items { get; } = new();

    public Task<T?> GetAsync<T>(string key) =>
        Task.FromResult(Items.TryGetValue(key, out var json) ? JsonSerializer.Deserialize<T>(json) : default);

    public Task SetAsync<T>(string key, T value)
    {
        Items[key] = JsonSerializer.Serialize(value);
        return Task.CompletedTask;
    }
}