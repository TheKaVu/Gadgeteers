using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [DisallowMultipleComponent]
    public class StatController : MonoBehaviour
    {
        [SerializeField]
        [SerializeReference, SubclassSelector]
        private List<Stat> _stats;

        public Collector<Modifier> ModifierCollector { get; } = new();

        public float this[KeyString key]
        {
            get => SafeGet(key);
			set 
			{
				TryGet(key, out var stat);
				stat?.SetValue(value, this);
			}
        }
        
        public bool Contains(KeyString key, bool searchParents = false) => GetStats(searchParents).Any(s => s.Key == key);

        public void RequireStats(params KeyString[] keys)
        {
            foreach (var key in keys)
            {
                if(!Contains(key)) throw new MissingStatException($"{gameObject.name} must have \"{key}\" stat!");
            }
        }
        
        public bool TryGet(KeyString key, out Stat stat)
        {
            foreach (var entry in GetStats(true))
            {
                if (entry.Key == key)
                {
                    stat = entry;
                    return true;
                }
            }
            stat = null;
            return false;
        }

        public float SafeGet(KeyString key)
        {
            if(TryGet(key, out var stat))
            {
                return Compute(stat);
            }
            return 0;
        }

        public Stat[] GetStats(bool includeParents = false)
        {
            var stats = new List<Stat>(_stats);
            if (includeParents && transform.parent != null && transform.parent.TryGetComponent(out StatController parentCtrl))
                stats.AddRange(parentCtrl.GetStats(true));
            return stats.ToArray();
        }

        public Modifier[] GetModifiers(bool includeParents = false)
        {
            var modifiers = ModifierCollector.Invoke();
            if (includeParents && transform.parent && transform.parent.TryGetComponent(out StatController parent))
                modifiers.AddRange(parent.GetModifiers(true));
            modifiers.Sort();
            return modifiers.ToArray();
        }

        public float Compute(Stat stat, bool ignoreModifiers = false)
        {
            var result = stat.BaseValue + stat.Dependencies.Sum(r => SafeGet(r.Target) * r.Factor);
            if (ignoreModifiers || stat.IgnoreModifiers) return result;
            foreach (var mod in GetModifiers(true).Where(m => stat.Key.BelongsTo(m.Target)))
            {
                if(!mod.FlagFilter.Enabled || (stat.Flags & mod.FlagFilter.Value) != 0)
                {
                    result = mod.Process(result);
                }
            }
            return result;
        }

        public void Load(List<Stat> stats)
        {
            _stats = new List<Stat>(stats);
        }

        public void LogStats()
        {
            var stats = GetStats(true);
            if(stats.Length == 0) Debug.Log("No statistics found.");
            foreach (var stat in stats)
            {
                Debug.Log($"{stat.Key} = {Compute(stat)}");
            }
        }

        public void LogMods()
        {
            var mods = GetModifiers(true);
            if(mods.Length == 0) Debug.Log("No modifiers found.");
            foreach (var mod in mods)
            {
                Debug.Log(mod.ToString());
            }
        }
    }
}