using System.Collections.Generic;
using UnityEngine;

namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path,"Stick Extension")]
    public class StickExtensionEffect : EffectUnit
    {
        public const float AttackRangeBonus = 0.2f;

        public override void OnStart()
        {
            Holder.StatCtrl.ModifierCollector.Remove(GetModifiers);
            Holder.StatCtrl.ModifierCollector.Add(GetModifiers);
            if(Stack < 6)
                Stack ++;
        }

        public override void OnTick()
        {
        }

        public override void OnEnd()
        {
            Holder.StatCtrl.ModifierCollector.Remove(GetModifiers);
        }

        private IEnumerable<Modifier> GetModifiers()
        {
            return new List<Modifier>{new(Operation.Add, Stack * AttackRangeBonus, "attack_range")};
        }
    }
}