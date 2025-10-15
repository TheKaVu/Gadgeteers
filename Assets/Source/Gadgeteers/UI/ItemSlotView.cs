using Source.Gadgeteers.Game;
using Source.Gadgeteers.Game.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Gadgeteers.UI
{
    public class ItemSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private int _slot;
        [SerializeField]
        private Image _display;
        
        [field: SerializeField]
        public Inventory Inventory {get; private set;}
        
        public int Slot => _slot;

        public void AssignTo(Inventory inventory)
        {
            Inventory = inventory;
        }

        private void Update()
        {
            _display.color = Inventory[_slot] is null ? Color.clear : Color.white;
            _display.sprite = Inventory[_slot] is null ? null : Inventory[_slot].Sprite;
            if (Inventory[_slot] is Gadget gadget && !gadget.Cooldown.IsReady)
            {
                _display.color = Color.gray;
                _display.fillAmount = gadget.Cooldown.Passed / gadget.Cooldown.Value;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var item = Inventory[_slot];
            if(item == null) return;
            Tooltip.Instance.ShowFor(item);
            if (item is IHasHitbox hasHitbox)
            {
                hasHitbox.Hitbox.SetMarkerVisible(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.Instance.SetVisible(false);
            var item = Inventory[_slot];
            if(item == null) return;
            if (item is IHasHitbox hasHitbox)
            {
                hasHitbox.Hitbox.SetMarkerVisible(false);
            }
        }
    }
}