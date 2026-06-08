using System.Text.Json.Serialization;

namespace DotaParser.Application.Common.Models.Constants;

public class HeroConstant
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("localized_name")]
    public string LocalizedName { get; set; } = string.Empty;

    [JsonPropertyName("primary_attr")]
    public string PrimaryAttribute { get; set; } = string.Empty;

    [JsonPropertyName("attack_type")]
    public string AttackType { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new();

    /// <summary>
    /// URL картинки героя (большой)
    /// </summary>
    [JsonPropertyName("img")]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// URL иконки героя (маленькой)
    /// </summary>
    [JsonPropertyName("icon")]
    public string IconUrl { get; set; } = string.Empty;

    [JsonPropertyName("base_health")]
    public int BaseHealth { get; set; }

    [JsonPropertyName("base_mana")]
    public int BaseMana { get; set; }

    [JsonPropertyName("base_armor")]
    public double BaseArmor { get; set; } // В JSON это int или double (например 2.5), лучше использовать double

    [JsonPropertyName("base_attack_min")]
    public int BaseAttackMin { get; set; }

    [JsonPropertyName("base_attack_max")]
    public int BaseAttackMax { get; set; }

    [JsonPropertyName("move_speed")]
    public int MoveSpeed { get; set; }
}