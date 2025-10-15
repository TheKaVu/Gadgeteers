using System;
using System.Collections.Generic;

namespace Source.Gadgeteers.Game.Items
{
    [Prefab(Path, "Cosmic Battery")]
    public class CosmicBattery : GadgetComponent
    {
        private float _bonus;
        
        private IEnumerable<Modifier> GetDynamicModifiers()
        {
            return new List<Modifier>{new(Operation.Add, _bonus, "power")};
        }

        private void Start()
        {
            StatCtrl.RequireStats("power.bonus");
        }

        private void Update()
        {
            _bonus = StatCtrl["power.bonus"];
        }

        public override void OnEquip()
        {
            ParentGadget?.StatCtrl.ModifierCollector.Add(GetDynamicModifiers);
        }

        public override void OnUnequip()
        {
            ParentGadget?.StatCtrl.ModifierCollector.Remove(GetDynamicModifiers);
        }
    }
}