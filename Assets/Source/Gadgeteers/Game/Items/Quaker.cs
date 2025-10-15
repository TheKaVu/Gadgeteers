using System.Collections;
using Source.Gadgeteers.Game.Effects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Quaker")]
    public class Quaker : Gadget, IHasHitbox
    {
        [SerializeField]
        private Hitbox _hitbox;
        
        public Hitbox Hitbox => _hitbox;
        
        public override void OnEquip()
        {
            var effect = Owner.EffectCtrl.Apply<QuakerChargeEffect>(Owner, StatCtrl["duration.stack"]);
            effect.QuakerItem = this;
        }

        public override void OnUnequip()
        {
            Owner.EffectCtrl.Clear<QuakerChargeEffect>(Owner);
        }

        protected override IEnumerator OnUse(InputAction.CallbackContext context)
        {
            if (!Owner.EffectCtrl.TryGetUnit<QuakerChargeEffect>(Owner, out var unit)) yield break;
            if (unit.Stack > 0)
            {
                Hitbox.Targets.ForEach(e =>
                {
                    if(e is IDamageable enemy) enemy.Damage(StatCtrl["damage.hit"] * unit.Stack, Owner, DamageType.Laser);
                });
                if(unit.Stack == 3)
                {
                    var e = Owner.EffectCtrl.Apply<QuakerChargeEffect>(unit.Source, StatCtrl["duration.stack"]);
                    e.QuakerItem = this;
                }
                unit.Stack = 0;
                yield break;
            }
            Debug.Log("Can't discharge - no Electrical Charge");
            Cooldown.Cancel();
        }
    }
}