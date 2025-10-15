using UnityEngine;

namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path,"Poison")]
    public class PoisonEffect : EffectUnit
    {
        public const float Dps = 20f;

        public override void OnStart()
        {
        }

        public override void OnTick()
        {
            if (Holder is IDamageable damageable)
            {
                damageable.Damage(Dps / Frequency, Source, DamageType.Internal);
            }
        }

        public override void OnEnd()
        {
        }
    }
}