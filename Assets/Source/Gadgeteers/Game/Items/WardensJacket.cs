using System.Collections;
using Source.Gadgeteers.Game.Effects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path,"Wardens Jacket")]
    public class WardensJacket : Gadget
    {
        protected override IEnumerator OnUse(InputAction.CallbackContext context)
        {
            var effect = Owner.EffectCtrl.Apply<UnbreakableEffect>(Owner, StatCtrl["duration"]);
            effect.WardensJacket = this;
            yield break;
        }
    }
}