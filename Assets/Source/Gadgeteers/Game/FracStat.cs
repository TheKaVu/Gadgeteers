using System;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public sealed class FracStat : Stat
    {
        [Tooltip("Key of parent statistic from which the max value will driven")]
        [SerializeField]
        private KeyString _parent;
        [Tooltip("Percentage value of parent statistic.")]
        [SerializeField]
        [Range(0f, 1f)]
        private float _factor;
        
        public override float BaseValue => 0;
        public override bool IsDynamic => true;
        public override bool IgnoreModifiers => true;
        public override StatDependency[] Dependencies => new[] { new StatDependency(_parent, _factor) };

        public FracStat()
        {
        }

        public FracStat(KeyString key, KeyString parent, float factor) : base(key)
        {
            _parent = parent;
            _factor = factor;
        }
        
        protected override bool TrySet(float value, StatController setter)
        {
            var max = setter[_parent];
            value = Mathf.Clamp(value, 0, max);
            _factor = value / max;
            return true;
        }
    }
}