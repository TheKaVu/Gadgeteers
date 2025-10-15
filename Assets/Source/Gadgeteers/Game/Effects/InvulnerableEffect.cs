namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path, "Invulnerable")]
    public class InvulnerableEffect : EffectUnit
    {
        private static void DamageHandle(ref Damage damage)
        {
            damage.Cancel();
        }
        
        public override void OnStart()
        {
            if (Holder is IDamageable damageable)
            {
                damageable.DamageHandler += DamageHandle;
            }
        }

        public override void OnTick()
        {
        }

        public override void OnEnd()
        {
            if (Holder is IDamageable damageable)
            {
                damageable.DamageHandler -= DamageHandle;
            }
        }
    }
}