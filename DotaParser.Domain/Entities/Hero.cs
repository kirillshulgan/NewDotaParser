namespace DotaParser.Domain.Entities;

public class Hero
{
    public int Id { get; set; }                 // PK
    public string Name { get; set; } = "";      // "npc_dota_hero_antimage"
    public string LocalizedName { get; set; } = "";
    public string? PrimaryAttr { get; set; }
    public string? AttackType { get; set; }
    public string? Img { get; set; }
    public string? Icon { get; set; }
    public int BaseHealth { get; set; }
    public float BaseHealthRegen { get; set; }
    public int BaseMana { get; set; }
    public float BaseManaRegen { get; set; }
    public float BaseArmor { get; set; }
    public int BaseMr { get; set; }
    public int BaseAttackMin { get; set; }
    public int BaseAttackMax { get; set; }
    public int BaseStr { get; set; }
    public int BaseAgi { get; set; }
    public int BaseInt { get; set; }
    public float StrGain { get; set; }
    public float AgiGain { get; set; }
    public float IntGain { get; set; }
    public int AttackRange { get; set; }
    public int ProjectileSpeed { get; set; }
    public float AttackRate { get; set; }
    public float BaseAttackTime { get; set; }
    public float AttackPoint { get; set; }
    public int MoveSpeed { get; set; }
    public float? TurnRate { get; set; }
    public bool CmEnabled { get; set; }
    public int Legs { get; set; }
    public int DayVision { get; set; }
    public int NightVision { get; set; }

    // Stats (из heroStats endpoint)
    public int? TurboPicks { get; set; }
    public int? TurboWins { get; set; }
    public int? ProBan { get; set; }
    public int? ProWin { get; set; }
    public int? ProPick { get; set; }

    // Navigation
    public ICollection<HeroRole> Roles { get; set; } = [];
    public ICollection<PlayerMatch> PlayerMatches { get; set; } = [];
    public ICollection<PlayerHero> PlayerHeroes { get; set; } = [];
    public ICollection<PlayerRanking> Rankings { get; set; } = [];
    public ICollection<PickBan> PicksBans { get; set; } = [];
}