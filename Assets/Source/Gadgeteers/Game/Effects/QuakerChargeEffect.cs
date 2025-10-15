using Source.Gadgeteers.Game.Items;

namespace Source.Gadgeteers.Game.Effects
{
    [Prefab(Path,"Quaker Charge")]
    public class QuakerChargeEffect : EffectUnit
    {
        public Quaker QuakerItem {get; set;}

        public override void OnStart()
        {
        }

        public override void OnTick()
        {
            QuakerItem.StatCtrl["energy_cost"] = Stack * QuakerItem.StatCtrl["energy_cost.per_stack"];
        }

        public override void OnEnd()
        {
            if(Stack < 3) 
                Stack ++;
            var e = Holder.EffectCtrl.Apply<QuakerChargeEffect>(Source, Stack < 3 ? QuakerItem.StatCtrl["duration.stack"] : -1, EffectReinitMode.Restart);
        }
    }
}