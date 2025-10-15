namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path,"Default")]
    public class DefaultEffect : EffectUnit
    {
        public override void OnStart()
        {
        }

        public override void OnTick()
        {
            var ctrl = Holder.StatCtrl;
            ctrl["health"] += ctrl["health_regen"] / Frequency;
            ctrl["energy"] += ctrl["energy_regen"] / Frequency;
        }

        public override void OnEnd()
        {
        }
    }
}