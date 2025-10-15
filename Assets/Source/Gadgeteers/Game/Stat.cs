using System;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public abstract class Stat
    {
        [SerializeField]
        private KeyString _key;
        [SerializeField]
        private Optional<int> _flags;

        public KeyString Key => _key;
        public int Flags => _flags.Enabled ? _flags.Value : 0;
        public abstract float BaseValue { get; }
        public abstract bool IsDynamic { get; }
        public abstract bool IgnoreModifiers { get; }
        public abstract StatDependency[] Dependencies { get; }
        
        public event Action<float, float> OnValueChanged; 

        protected Stat() {}
        
        protected Stat(KeyString key)
        {
            _key = key;
        }

        public bool SetValue(float value, StatController setter)
        {
            var oldValue = setter.Compute(this, true);
            var changed = TrySet(value, setter);
            if (changed) OnValueChanged?.Invoke(oldValue, value);
            return changed;
        }
        
        protected abstract bool TrySet(float value, StatController setter);

        public override string ToString()
        {
            return _key;
        }

        public float ComputeWith(StatController statCtrl)
        {
            return statCtrl.Compute(this);
        }
    }
}