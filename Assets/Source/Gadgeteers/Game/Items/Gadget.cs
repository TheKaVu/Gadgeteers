using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game.Items
{
    [RequireComponent(typeof(Inventory))]
    public abstract class Gadget : Item, IEquipable
    {
        public float EnergyCost => StatCtrl["energy_cost"];
        public Cooldown Cooldown { get; private set; }
        public Inventory Components { get; private set; }
        
        protected new void Awake()
        {
            base.Awake();
            
            StatCtrl.RequireStats("cooldown","energy_cost","power");
            
            Cooldown = new Cooldown(() => StatCtrl["cooldown"]);
            Components = GetComponent<Inventory>();
            StatCtrl.ModifierCollector.Add(Components.GetModifiers);
        }

        public void Use(InputAction.CallbackContext context)
        {
            if(!Cooldown.IsReady || StatCtrl["energy"] <= EnergyCost || !CanUse()) return;
            StatCtrl["energy"] -= EnergyCost;
            Cooldown.Begin();
            StartCoroutine(OnUse(context));
        }

        protected abstract IEnumerator OnUse(InputAction.CallbackContext context);

        protected virtual bool CanUse() => true;
        public virtual void OnEquip(){}
        public virtual  void OnUnequip(){}
    }
}