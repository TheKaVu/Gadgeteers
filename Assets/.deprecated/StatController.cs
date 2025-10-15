using Assets.Scripts.Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    [DisallowMultipleComponent]
    [Serializable]
    public class StatController : NetworkBehaviour
    {
        private readonly Dictionary<string, Stat> _stats = new();

        [SerializeField]
        private List<Stat> _statsList;
        [SerializeField]
        private List<StatSubscriber> _subscribers;
        [SerializeField]
        private bool _logStatsOnInit;

        public ValueCollector<StatModifier> Modifiers { get; } = new();
        public List<StatSubscriber> Subscribers => _subscribers;

        public event Action<string, float> StatisticChanged;

        public float this[StatType stat] => GetFirst(stat);
        public float this[string key] => Get(key);

        private void Awake()
        {
            LoadStats();
        }

        private void OnValidate()
        {
            _stats.Clear();
            for(int i = 0; i < _statsList.Count; i++)
            {
                var stat = _statsList[i];
                if (!_stats.TryAdd(stat.Key, stat)) throw new DuplicateElementException($"Statistic of key {stat.Key} already exists! If the game starts, it will be removed!");
            }
        }

        private void Compute(ICollection<StatType> affectedStats)
        {
            List<StatModifier> mods = new();
            HashSet<string> modified = new();

            foreach (var s in _subscribers)
            {
                mods.AddRange(s.ModifierCollector.Collect<List<StatModifier>>(m => affectedStats.Contains(m.Target)));
            }
            mods.Sort();

            foreach (var mod in mods)
            {
                foreach (var stat in GetStats(mod.Target))
                {
                    if (!modified.Contains(stat.Key))
                    {
                        SetValue(stat.Key, stat.BaseValue);
                        modified.Add(stat.Key);
                    }
                    SetValue(stat.Key, mod.Modify(stat.Value));
                }
            }
        }

        private void SetValue(string key, float newValue)
        {
            StatisticChanged(key, newValue);
        }

        public void LoadStats()
        {
            _stats.Clear();
            for (int i = 0; i < _statsList.Count; i++)
            {
                var stat = _statsList[i];
                if (_stats.ContainsKey(stat.Key))
                {
                    Debug.LogWarning($"A duplicate statistic of key {stat.Key} was found and removed.");
                    _statsList.RemoveAt(i);
                    i--;
                }
                else
                {
                    stat.LinkTo(this);
                    _stats.Add(stat.Key, stat);
                }
            }

            foreach (var s in _subscribers)
            {
                s.ModifiersChanged += Compute;
            }
            

            if (_logStatsOnInit)
            {
                LogStats();
            }
        }

        public float GetFirst(StatType statType)
        {
            return Get(statType.ToString().ToLower());
        }

        public float Get(string key)
        {
            return _stats[key].Value;
        }

        public bool Contains(string key)
        {
            return _stats.ContainsKey(key);
        }

        public Stat GetStat(string key)
        {
            return _stats[key];
        }

        public HashSet<Stat> GetStats(StatType type)
        {
            HashSet<Stat> stats = new();
            foreach (var stat in _stats.Values)
            {
                if(stat.StatType == type) stats.Add(stat);
            }
            return stats;
        }

        public HashSet<StatType> GetPresentTypes()
        {
            return _stats.Values.Select(s => s.StatType).Distinct().ToHashSet();
        }

        public void Subscribe(StatSubscriber subscriber) => _subscribers.Add(subscriber);

        public void Unsubscribe(StatSubscriber subscriber) => _subscribers.Remove(subscriber);
        public void LogStats()
        {
            foreach (var s in _stats)
            {
                Debug.Log($"{s.Key}: {s.Value.Value}");
            }
        }
    }
}
