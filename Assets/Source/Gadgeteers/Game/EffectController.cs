using System;
using System.Collections.Generic;
using System.Linq;
using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [DisallowMultipleComponent]
    public class EffectController : MonoBehaviour
    {
        private readonly List<EffectUnit> _units = new();

        public EffectUnit[] Units => _units.ToArray();

        public bool HasEffect<T>() where T : EffectUnit
        {
            return HasEffect(typeof(T));
        }

        public bool HasEffect(Type type, Entity source = null)
        {
            return _units.FindAll(e => e.GetType() == type && (e.Source == source || !source)).Any();
        }
        
        public bool TryGetUnit<T>(Entity source, out EffectUnit unit) where T : EffectUnit => TryGetUnit(typeof(T), source, out unit);

        public bool TryGetUnit(Type t, Entity source, out EffectUnit unit)
        {
            unit = _units.Find(unit => unit.GetType() == t && unit.Source == source);
            return unit != null;
        }

        public EffectUnit[] GetUnits<T>() where T : EffectUnit => GetUnits(typeof(T));

        public EffectUnit[] GetUnits(Type t)
        {
            return _units.FindAll(unit => unit.GetType() == t).ToArray();
        }

        public T Apply<T>(Entity source, float duration, EffectReinitMode mode = EffectReinitMode.Replace) where T : EffectUnit
        {
            var t = typeof(T);
            if (TryGetUnit(t, source, out var unit))
            {
                if (mode != EffectReinitMode.Replace)
                {
                    unit.Reinit(duration, mode);
                    return (T)unit;
                }
                Clear(t, source, true);
            }

            var effect = Util.CreateFromPrefab<T>();
            effect.transform.SetParent(transform);
            _units.Add(effect);
            effect.Init(source, duration);
            return effect;
        }
        
        public void Clear<T>(Entity source, bool force = false) where T : EffectUnit
        {
            Clear(typeof(T), source, force);
        }

        public void Clear<T>(bool force = false) where T : EffectUnit
        {
            Clear(typeof(T), force);
        }

        public void Clear(Type t, Entity source, bool force = false)
        {
            if (TryGetUnit(t, source, out var unit))
            {
                if (!unit.TryEnd() && !force) return;
                _units.Remove(unit);
                Destroy(unit.gameObject);
            }
        }

        public void Clear(Type t, bool force = false)
        {
            _units.ForEach(unit =>
            {
                if (unit.GetType() == t) Clear(t, unit.Holder, force);
            });
        }
    }
}