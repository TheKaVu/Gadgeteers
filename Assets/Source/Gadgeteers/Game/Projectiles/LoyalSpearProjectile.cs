using Source.Gadgeteers.Game.Entities;

namespace Source.Gadgeteers.Game.Projectiles
{
    [Prefab(Path, "Loyal Spear")]
    public class LoyalSpearProjectile : Projectile
    {
        public float Damage { get; set; }
        
        protected override void OnHit(Entity target)
        {
            if (target is IDamageable enemy)
            {
                enemy.Damage(Damage, Sender, DamageType.Ballistic);
                Break();
            }
        }
    }
}