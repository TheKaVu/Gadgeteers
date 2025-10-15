using System.Collections;
using Source.Gadgeteers.Game.Projectiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Loyal Spear")]
    public class LoyalSpear : Gadget, IHasHitbox
    {
        [SerializeField]
        private Hitbox _hitbox;
        [SerializeField]
        private LoyalSpearProjectile _projectile;
        
        public Hitbox Hitbox => _hitbox;

        protected override IEnumerator OnUse(InputAction.CallbackContext context)
        {
            if(StatCtrl.TryGet("damage.hit", out var dmgStat))
            {
                var p = Instantiate(_projectile, Owner.transform.position, Owner.transform.rotation);
                p.Send(Owner);
                p.Damage = StatCtrl.Compute(dmgStat);
            }
            yield break;
        }
    }
}