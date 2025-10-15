using System;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    public class PercentStatModifier : StatModifier
    {
        public PercentStatModifier() : base(0) { }

        public PercentStatModifier(StatType target, float value) : base(target, value, 0) { }


        public override float Modify(float value)
        {
            return value * Value;
        }
        public override string ToString()
        {
            return string.Concat(Value >= 1 ? "+" : "", $"{(Value - 1) * 100}%{Target}");
        }
    }
}
