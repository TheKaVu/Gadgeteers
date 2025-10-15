using Source.Gadgeteers.Game.Entities;
using Source.Gadgeteers.Game.Items;

namespace Source.Gadgeteers.Game
{
    public interface IDamageable
    {
        public DamageHandler DamageHandler { get; set; }
        
        public float Damage(float damage, Entity damager, DamageType damageType);
    }
}
