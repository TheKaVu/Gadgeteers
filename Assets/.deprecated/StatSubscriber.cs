using Assets.Scripts.Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    [DisallowMultipleComponent]
    public class StatSubscriber : NetworkBehaviour
    {
        public event Action<ICollection<StatType>> ModifiersChanged;

        [SerializeReference, SubclassSelector]
        private List<StatModifier> _staticModifiers;

        public ValueCollector<StatModifier> ModifierCollector { get; } = new();

        private void Awake()
        {
            ModifierCollector.Supply += () => _staticModifiers;
        }

        public void OnModifiersChanged(params StatType[] affectedStats)
        {
            ModifiersChanged?.Invoke(affectedStats.ToHashSet());
        }
    }
}
