namespace Source.Gadgeteers.Game.Items
{
    public abstract class GadgetComponent : Item, IEquipable
    {
        public Gadget ParentGadget => GetComponentInParent<Gadget>();
        
        public virtual void OnEquip() {}
        
        public virtual void OnUnequip() {}
    }
}