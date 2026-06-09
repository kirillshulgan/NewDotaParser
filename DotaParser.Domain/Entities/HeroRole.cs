namespace DotaParser.Domain.Entities;

public class HeroRole
{
    public int HeroId { get; set; }
    public string Role { get; set; } = "";      // "Carry", "Support", etc.

    public Hero Hero { get; set; } = null!;
}