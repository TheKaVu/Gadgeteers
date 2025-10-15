using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Crushers")]
    public class Crushers : Gadget, IHasHitbox
    {
        [SerializeField]
        private Hitbox _hitbox;
        
        public Hitbox Hitbox => _hitbox;

        protected override IEnumerator OnUse(InputAction.CallbackContext context)
        {
            var stun = StatCtrl["duration.stun"];
            
            if(StatCtrl.TryGet("damage.hit", out var dmgStat))
            {
                foreach (var target in _hitbox.Targets)
                {
                    if (target is not IDamageable enemy) continue;
                    enemy.Damage(StatCtrl.Compute(dmgStat), Owner, (DamageType)dmgStat.Flags);
                }
            }
            
            Debug.Log($"Stunned {_hitbox.Targets.Count} target(s) for {stun}");
            yield break;
        }
    }
}