using Source.Gadgeteers.Game.Effects;
using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Knuckles")]
    public class Knuckles : Weapon
    {
        [SerializeField]
        private AnimationClip[] _punchAnimations;

        private int _index = 2;
        
        protected override void OnHit(Entity target)
        {
            if (target is IDamageable enemy && StatCtrl.TryGet("damage.hit", out var dmg))
            {
                enemy.Damage(StatCtrl.Compute(dmg), Owner, (DamageType)dmg.Flags);
            }
        }

        protected override AnimationClip GetAttackAnimation()
        {
            if(_index == 2)
                Owner.EffectCtrl.Apply<KnucklesRestEffect>(Owner, Cooldown.Value * 1.5f);
            var a = _punchAnimations[_index];
            _index = (_index + 1) % _punchAnimations.Length;
            return a;
        }

        protected override bool CanUse() => !Owner.EffectCtrl.HasEffect<KnucklesRestEffect>();
    }
}