using System;
using Source.Gadgeteers.UnityEditor;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public struct StatDependency
    {
        [SerializeField]
        private KeyString _target;
        [SerializeField, Min(0)]
        private float _factor;

        public StatDependency(KeyString target, float factor)
        {
            _target = target;
            _factor = factor;
        }

        public KeyString Target => _target;
        public float Factor => _factor;

        public override string ToString()
        {
            return $"{(_factor >= 0 ? "+" : string.Empty)}{_factor * 100}%{_target}";
        }
    }
}
