using System;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    public abstract class StatModifier : IComparable<StatModifier>
    {
        [SerializeField]
        private StatType _target;
        [SerializeField]
        private float _value;
        
        protected int Priority { get; }

        public float Value => _value;
        public StatType Target => _target;

        protected StatModifier(StatType target, float value, int priority)
        {
            _target = target;
            _value = value;
            Priority = priority;
        }

        protected StatModifier(int priority) {
            Priority = priority;
        }

        public abstract float Modify(float value);

        public int CompareTo(StatModifier other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}
