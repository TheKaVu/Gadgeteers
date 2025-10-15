using Source.Gadgeteers.Game.Effects;
using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Extendo Stick")]
    public class ExtendoStick : Weapon
    {
        private void Start()
        {
            AllowMultipleTargets = false;
        }
        
        protected override void OnHit(Entity enemy)
        {
            if (enemy is IDamageable damageable)
            {
                damageable.Damage(StatCtrl["damage.hit"], Owner, DamageType.Impact);
            }
            
            var mode = EffectReinitMode.Restart;
            if (Owner.EffectCtrl.TryGetUnit<StickExtensionEffect>(Owner, out var unit))
            {
                if(unit.Stack >= 6) 
                    mode = EffectReinitMode.Restart;
            }
            Owner.EffectCtrl.Apply<StickExtensionEffect>(Owner, StatCtrl["duration.threshold"], mode);
        }

        protected override AnimationClip GetAttackAnimation()
        {
            return null;
        }
    }
}