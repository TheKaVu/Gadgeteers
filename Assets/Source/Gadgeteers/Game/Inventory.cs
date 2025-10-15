using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Gadgeteers.Game.Entities;
using Source.Gadgeteers.Game.Items;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public class Inventory : MonoBehaviour, IEnumerable<Item>
    {
        private Item[] _items;
        
        [SerializeField, Min(1)]
        private int _size;
        
        private Entity _holder;
        
        public int Size => _size;
        
        public Item this[int slot] => GetItem(slot);

        private void Awake()
        {
            _items = new Item[_size];
            _holder = GetComponent<Entity>();
        }
        
        public Item GetItem(int slot) => _items[slot];
        
        public Item Put(Item item, int slot)
        {
            var old = _items[slot];
            _items[slot] = item;
            item.transform.SetParent(transform);
            if(slot < 5)
            {
                if(item is IEquipable eItem)
                {
                    item.transform.SetParent(_holder.transform);
                    eItem.OnEquip();
                }
                if(old is IEquipable eOld)
                {
                    item.transform.SetParent(null);
                    eOld.OnUnequip();
                }
            }
            old?.transform.SetParent(null);
            return old;
        }

        public void Swap(int slotA, int slotB)
        {
            var a = _items[slotA];
            var b = _items[slotB];
            Put(a, slotB);
            Put(b, slotA);
        }

        public Item Clear(int slot)
        {
            var old = _items[slot];
            _items[slot] = null;
            old.transform.SetParent(null);
            return old;
        }

        public Modifier[] GetModifiers()
        {
            return GetModifiers(0, _items.Length - 1);
        }
        
        public Modifier[] GetModifiers(int from, int to)
        {
            if(!enabled) return Array.Empty<Modifier>();
            var mods = new List<Modifier>();
            for (var i = from; i <= to; i++)
            {
                var item = _items[i];
                if(item == null) continue;
                mods.AddRange(item.GetModifiers());
            }
            return mods.ToArray();
        }
        
        public IEnumerator<Item> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}