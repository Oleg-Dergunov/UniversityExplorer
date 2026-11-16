using Microsoft.JSInterop;
using System.Text.Json;

namespace UniversityExplorer.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _js;

    public LocalStorageService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _js.InvokeAsync<string?>("localStorage.getItem", key);

        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException)
        {
            // Corrupted data in storage is treated as missing
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }
}