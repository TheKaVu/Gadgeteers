using Source.Gadgeteers.Game.Entities;

namespace Source.Gadgeteers.Game.Events
{
    public class DamageDealtEvent : IEvent
    {
        public Entity Damager { get; }
        public IDamageable Target { get; }
        public Damage Damage { get; }

        public DamageDealtEvent(Entity damager, IDamageable target, Damage damage)
        {
            Damager = damager;
            Target = target;
            Damage = damage;
        }
    }
}