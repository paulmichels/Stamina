interface IStatusEffect
{
    void Stuned();
    void Bleeding();
    void ShieldBreaked();
}

interface IStatusAbility
{
    void Stun();
    void Bleed();
    void ShieldBreak();
}

interface ICharacterStatistics
{
    int Health { get; set; }
    int HealthMax { get; set; }
    int Level { get; set; }
    int Armor { get; set; }
    int ArmorMax { get; set; }
}

interface IPlayerStatistics
{
    int Experience { get; set; }
    int Mana { get; set; }
    int ManaMax { get; set; }
}