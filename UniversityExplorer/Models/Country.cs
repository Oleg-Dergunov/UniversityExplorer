using System.Text.Json.Serialization;

namespace UniversityExplorer.Models;

public class Country
{
    public string Name { get; set; } = string.Empty;

    // Some territories (e.g. Antarctica) have no capital
    public string? Capital { get; set; }

    public string? Region { get; set; }

    public string? Subregion { get; set; }

    public long Population { get; set; }

    // Emoji flags are not rendered on Windows, so Flags.Png is the primary flag
    [JsonPropertyName("flag")]
    public string? FlagEmoji { get; set; }

    public CountryFlags? Flags { get; set; }

    public string? Alpha2Code { get; set; }
}