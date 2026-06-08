using System.Text.Json.Serialization;

namespace DotaParser.Infrastructure.Models;

public class OpenDotaProfile
{
    [JsonPropertyName("account_id")] 
    public long AccountId { get; set; }

    [JsonPropertyName("personaname")] 
    public string PersonaName { get; set; } = string.Empty;

    [JsonPropertyName("name")] 
    public string? Name { get; set; }

    [JsonPropertyName("plus")] 
    public bool? Plus { get; set; }

    [JsonPropertyName("cheese")] 
    public int? Cheese { get; set; }

    [JsonPropertyName("steamid")] 
    public string SteamId { get; set; } = string.Empty;

    [JsonPropertyName("avatar")] 
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("avatarmedium")] 
    public string AvatarMedium { get; set; } = string.Empty;

    [JsonPropertyName("avatarfull")] 
    public string AvatarFull { get; set; } = string.Empty;

    [JsonPropertyName("profileurl")] 
    public string ProfileUrl { get; set; } = string.Empty;

    [JsonPropertyName("last_login")] 
    public string? LastLogin { get; set; }

    [JsonPropertyName("loccountrycode")] 
    public string? LocCountryCode { get; set; }

    [JsonPropertyName("is_contributor")] 
    public bool? IsContributor { get; set; }

    [JsonPropertyName("is_subscriber")] 
    public bool? IsSubscriber { get; set; }
}