namespace Assets.Scripts.Game.Statistics
{
    public enum StatType
    {
        Unknown = 0,

        // Vitality
        Vitality = 1,
        MaxHealth = 2,
        Health = 3,
        HealthRegen = 4,
        // Chargeflow
        Chargeflow = 5,
        MaxEnergy = 6,
        Energy = 7,
        EnergyRegen = 8,
        // Sustain
        Sustain = 9,
        Resistance = 10,
        Immunity = 11,
        // Condition
        Condition = 12,
        Strength = 13,
        Movespeed = 14,
        Accuracy = 15,
        // Gadget
        AttackRange = 16,
        AttackSpeed = 17,
        Power = 18,
        PhysicalDamage = 19,
        ChemicalDamage = 20,
        Cooldown = 21,
        EnergyCost = 22,
    }

    public static class StatTypeExtension
    {
        public static string AsKey(this StatType statType)
        {
            return statType.ToString().ToLower();
        }
    }
}
