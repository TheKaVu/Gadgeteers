using System;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    public class FlatStatModifier : StatModifier
    {
        public FlatStatModifier() : base(20) { }

        public FlatStatModifier(StatType target, float value) : base(target, value, 20) { }


        public override float Modify(float value)
        {
            return value + Value;
        }
        public override string ToString()
        {
            return string.Concat(Value >= 0 ? "+" : "", $"{Value} {Target}");
        }
    }
}
