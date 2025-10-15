using System;
using System.Collections.Generic;
using Source.Gadgeteers.Game.Items;
using UnityEngine;

namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path,"Unbreakable")]
    public class UnbreakableEffect : EffectUnit
    {
        public WardensJacket WardensJacket { get; set; } 

        private float _bonus;

        private IEnumerable<Modifier> GetModifiers()
        {
            return new List<Modifier>{new(Operation.Add, _bonus , "sustain")};
        }

        private void IncreaseBonus(float oldValue, float newValue)
        {
            if(oldValue > newValue) _bonus += (oldValue - newValue) * 0.2f;
            _bonus = MathF.Min(_bonus, WardensJacket.StatCtrl["sustain.max_bonus"]);
        }
        
        public override void OnStart()
        {
            Holder.StatCtrl.ModifierCollector.Add(GetModifiers);
            if (Holder.StatCtrl.TryGet("health", out var health))
            {
                health.OnValueChanged += IncreaseBonus;
            }
            _bonus = 0;
        }

        public override void OnTick()
        {
        }

        public override void OnEnd()
        {
            Holder.StatCtrl.ModifierCollector.Remove(GetModifiers);
            if (Holder.StatCtrl.TryGet("health", out var health))
            {
                health.OnValueChanged -= IncreaseBonus;
            }
        }
    }
}