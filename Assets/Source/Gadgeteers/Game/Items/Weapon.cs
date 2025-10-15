using System.Collections;
using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game.Items
{
    public abstract class Weapon : Item, IHasHitbox, IEquipable
    {
        private static readonly int ActionTriggerParameter = Animator.StringToHash("action");
        private static readonly int ActionSpeedCorrectionParameter = Animator.StringToHash("actionSpeedCorrection");

        [SerializeField]
        private Hitbox _hitbox;

        [SerializeField]
        private AnimatorOverrideController _attackAnimator;
        [SerializeField]
        private float _attackDelay;
        
        public Hitbox Hitbox => _hitbox;
        public Cooldown Cooldown { get; private set; }
        public float AttackSpeed => StatCtrl["attack_speed"];
        public float AttackRange => StatCtrl["attack_range"];
        public float Power => StatCtrl["power"];
        public bool AllowMultipleTargets { get; protected set; }
        
        protected new void Awake()
        {
            base.Awake();
            StatCtrl.RequireStats("attack_speed","attack_range","power");
            
            Cooldown = new Cooldown(() =>
            {
                var attackSpeed = StatCtrl["attack_speed"];
                return attackSpeed > 0 ? 1f / attackSpeed : float.MaxValue;
            });
        }

        private void Update()
        {
            var range = StatCtrl["attack_range"];
            _hitbox.transform.localScale = new Vector3(range, 0, range);
        }

        public void Attack()
        {
            if(!Cooldown.IsReady || !CanUse()) return;
            Cooldown.Begin();
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            OverrideAnimator();
            
            Owner.Animator.SetTrigger(ActionTriggerParameter);
            
            yield return new WaitForSeconds(_attackDelay);
            
            if(_hitbox.Targets.Count == 0) yield break;
            
            if (!AllowMultipleTargets)
            {
                OnHit(_hitbox.Targets[0]);
                yield break;
            }
            foreach (var target in _hitbox.Targets)
            {
                OnHit(target);
            }
        }

        private void OverrideAnimator()
        {
            var animator = Owner.Animator;
            animator.runtimeAnimatorController = _attackAnimator;
            var ac = GetAttackAnimation();
            if(ac == null) return;
            _attackAnimator["Action"] = ac;
            animator.SetFloat(ActionSpeedCorrectionParameter,ac.length / Cooldown.Value);
        }

        protected abstract void OnHit(Entity target);

        protected abstract AnimationClip GetAttackAnimation();
        
        protected virtual bool CanUse() => true;

        public virtual void OnEquip()
        {
            OverrideAnimator();
        }
        public virtual  void OnUnequip(){}
    }
}