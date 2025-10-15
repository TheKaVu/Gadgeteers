using System;
using System.Collections.Generic;
using System.Linq;
using Source.Gadgeteers.Game.Effects;
using Source.Gadgeteers.Game.Events;
using Source.Gadgeteers.Game.Items;
using UnityEngine;

namespace Source.Gadgeteers.Game.Entities
{
    [RequireComponent(typeof(Inventory))]
    public sealed class Player : Entity, IDamageable, ITeamMember
    {
        private static readonly int VelocityParameter = Animator.StringToHash("velocity");
        private static readonly int DeviationParameter = Animator.StringToHash("deviation");

        [SerializeField]
        private Animator _animator;
        
        public Animator Animator => _animator;
        public Inventory Inventory { get; private set; }
        public DamageHandler DamageHandler { get; set; }

        public float Damage(float damage, Entity damager, DamageType damageType)
        {
            var d = new Damage(damage, damageType);
            DamageHandler(ref d);
            StatCtrl[Stats.Health] -= d.Value;
            
            EventBus<DamageDealtEvent>.Call(this, new(damager, this, d));
            
            return d.Value;
        }

        protected override void OnAwake()
        {
            Inventory = GetComponent<Inventory>();
            
            StatCtrl.ModifierCollector.Add(() => Inventory.GetModifiers(0,4));
            
            DamageHandler += DefaultDamageHandle;
        }

        protected override void OnUpdate()
        {
            _animator.SetFloat(VelocityParameter, IsMoving ? StatCtrl[Stats.MoveSpeed] / 25 : 0);
            var deviation = Vector3.Angle(transform.forward, Direction);
            _animator.SetFloat(DeviationParameter, deviation);
        }

        private void Start()
        {
            EffectCtrl.Apply<DefaultEffect>(this, -1);
            StatCtrl.Load(CreateDefaultStats(0.5f, 0.5f, new Vector2(0.333f, 0.333f)));
        }

        private void DefaultDamageHandle(ref Damage damage)
        {
            float reduction = StatCtrl[Stats.Sustain];
            reduction /= reduction + Util.DamageReductionIndex;
            damage.Value *= 1 - reduction / (reduction + Util.DamageReductionIndex);
        }
        
        public static List<Stat> CreateDefaultStats(float vitalityIndex, float chargeFlowIndex, Vector2 conditionIndex)
        {
            if (vitalityIndex is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(vitalityIndex));
            if (chargeFlowIndex is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(chargeFlowIndex));
            if (conditionIndex.x + conditionIndex.y is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(conditionIndex));
            
            var stats = new List<Stat>
            {
                // Core stats
                new FlatStat(Stats.Vitality, 20, false),
                new FlatStat(Stats.ChargeFlow, 20, false),
                new FlatStat(Stats.Sustain, 20, false),
                new FlatStat(Stats.Condition, 15, false),
                // Secondary stats
                new FlatStat(Stats.MaxHealth, 100, false, new List<StatDependency>{new(Stats.Vitality, vitalityIndex * 20)}),
                new FlatStat(Stats.HealthRegen, 0.2f, false, new List<StatDependency>{new(Stats.Vitality, (1 - vitalityIndex) * 0.1f)}),
                new FracStat(Stats.Health, Stats.MaxHealth, 1),
                new FlatStat(Stats.MaxEnergy, 150, false, new List<StatDependency>{new(Stats.ChargeFlow, chargeFlowIndex * 40)}),
                new FlatStat(Stats.EnergyRegen, 0.5f, false, new List<StatDependency>{new(Stats.ChargeFlow, (1 - chargeFlowIndex) * 0.4f)}),
                new FracStat(Stats.Energy, Stats.MaxEnergy, 1),
                new FlatStat(Stats.Strength, 3, false, new List<StatDependency>{new(Stats.Condition, conditionIndex.x)}),
                new FlatStat(Stats.Accuracy, 0, false, new List<StatDependency>{new(Stats.Condition, conditionIndex.y * 5)}),
                new FlatStat(Stats.MoveSpeed, 2, false, new List<StatDependency>{new(Stats.Condition, (1 - conditionIndex.x - conditionIndex.y) * 0.6f)})
            };

            return stats;
        }
    }
}