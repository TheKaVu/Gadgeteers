using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public sealed class FlatStat : Stat
    {
        [Tooltip("Base value of this statistic.")]
        [SerializeField]
        private float _baseValue;
        [Tooltip("Determines whether the base value of the statistic can be changed through code.")]
        [SerializeField]
        private bool _isDynamic;
        [Tooltip("Determines if the modifiers should be ignored during computation.")]
        [SerializeField]
        private bool _ignoreModifiers;
        [Tooltip("All statistics this statistic depends on.")]
        [SerializeField]
        private List<StatDependency> _dependencies;

        public override float BaseValue => _baseValue;
        public override bool IsDynamic => _isDynamic;
        public override bool IgnoreModifiers => _ignoreModifiers;
        public override StatDependency[] Dependencies => _dependencies.ToArray();

        public FlatStat() {}
        
        public FlatStat(KeyString key, float baseValue, bool isDynamic, List<StatDependency> dependencies = null) :  base(key)
        {
            _baseValue = baseValue;
            _isDynamic = isDynamic;
            _dependencies = dependencies == null ? new List<StatDependency>() : new List<StatDependency>(dependencies);
        }

        protected  override bool TrySet(float value, StatController setter)
        {
            if (!_isDynamic) return false;
            _baseValue = value;
            return true;
        }
    }
}