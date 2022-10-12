using System;
using System.Collections.Generic;
using UnityEngine;
using VideoGameExperiments.Inventory;
using VideoGameExperiments.Items;

namespace TextRPG
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<KeyCode, int> _itemMap = new()
        {
            [KeyCode.Alpha1] = 1,
            [KeyCode.Alpha2] = 2,
            [KeyCode.Alpha3] = 3,
        };

        private BasicInventory _inventory;

        public int Size;

        public List<Sprite> ItemSprites;

        public event Action<BasicInventory> OnInventoryUpdate;

        private void Awake()
        {
            _inventory = new BasicInventory(Size);

            OnInventoryUpdate?.Invoke(_inventory);
        }

        private void Update()
        {
            var didChange = false;

            foreach (var (keyCode, id) in _itemMap)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    var (count, remaining) = _inventory.Collect(new IItem[]
                    {
                        CreateItem(id),
                    });

                    didChange |= count > 0;
                }
            }

            if (didChange)
            {
                OnInventoryUpdate?.Invoke(_inventory);
            }
        }

        public IItem Peek(int slot) => _inventory.Peek(slot);

        public (int, IItem[]) Add(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.Add(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate?.Invoke(_inventory);
            }

            return (count, remaining);
        }

        public (int, IItem[]) AddOrSwap(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.AddOrSwap(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate?.Invoke(_inventory);
            }

            return (count, remaining);
        }

        public IItem[] Remove(int count, int slot)
        {
            var items = _inventory.Remove(count, slot);
            if (items.Length > 0)
            {
                OnInventoryUpdate?.Invoke(_inventory);
            }

            return items;
        }

        public IItem[] RemoveHalf(int slot)
        {
            var count = _inventory.GetCount(slot);
            var removeCount = count % 2 == 0 ? count / 2 : count / 2 + 1;
            return Remove(removeCount, slot);
        }

        public IItem[] RemoveAll(int slot) => Remove(_inventory.GetCount(slot), slot);

        public void Clear()
        {
            _inventory.Clear();

            OnInventoryUpdate?.Invoke(_inventory);
        }

        public void Sort()
        {
            _inventory.Sort();

            OnInventoryUpdate?.Invoke(_inventory);
        }

        public Sprite GetSprite(IItem item)
        {
            var spriteIndex = item.Id - 1;
            return ItemSprites[spriteIndex];
        }

        public SlotView[] GetSlots() => GetComponentsInChildren<SlotView>();

        private static IItem CreateItem(int id) => id switch
        {
            1 => new Potion(),
            2 => new Coin(),
            3 => new Sword(),
            _ => throw new NotImplementedException(),
        };
    }
}
