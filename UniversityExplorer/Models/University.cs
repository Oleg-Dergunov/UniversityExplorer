using System.Text.Json.Serialization;

namespace UniversityExplorer.Models;

public class University
{
    public string Name { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("alpha_two_code")]
    public string? AlphaTwoCode { get; set; }

    [JsonPropertyName("state-province")]
    public string? StateProvince { get; set; }

    [JsonPropertyName("web_pages")]
    public List<string> WebPages { get; set; } = new();

    public List<string> Domains { get; set; } = new();


    [JsonIgnore]
    public string? Website => WebPages.FirstOrDefault();

    [JsonIgnore]
    public string? Domain => Domains.FirstOrDefault();

    [JsonIgnore]
    public string? WebsiteDisplay =>
    Uri.TryCreate(Website, UriKind.Absolute, out var uri) ? uri.Host : Website;
}