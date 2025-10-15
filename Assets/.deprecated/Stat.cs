using Assets.Scripts.Game.Utils;
using System;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    public class Stat
    {
        [SerializeField]
        private StatType _statType;
        [SerializeField]
        private string _name;
        [SerializeField]
        private float _baseValue;
        [SerializeField]
        [EditorReadOnly]
        private float _value;
        [SerializeField]
        private Optional<StatBinding> _binding;

        public StatController Controller { get; private set; }

        public StatType StatType => _statType;

        public float BaseValue => _baseValue + (_binding.Enabled ? _binding.Value.Get() : 0);

        public float Value => _value;

        public string Key => _statType.AsKey() + (_name == "" ? "" : $".{_name}");

        public Stat() { }

        public Stat(StatType statType, float baseValue)
        {
            _statType = statType;
            _baseValue = baseValue;
            _binding = new Optional<StatBinding>(default, false);
        }

        public Stat(StatType statType, float baseValue, StatBinding bind)
        {
            _statType = statType;
            _baseValue = baseValue;
            _binding = new Optional<StatBinding>(bind, true);
        }

        public void LinkTo(StatController controller)
        {
            if(controller.Contains(Key)) throw new DuplicateElementException($"Can't link {Key} stat to the controller because it already has one!");
            Controller = controller;
            controller.StatisticChanged += OnChange;
            _value = BaseValue;
        }

        private void OnChange(string key, float newValue)
        {
            if(key == Key) _value = newValue;
        }
    }
}
