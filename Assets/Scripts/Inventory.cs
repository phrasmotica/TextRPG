using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Inventory;
using VideoGameExperiments.Items;

namespace TextRPG
{
    public class Inventory : MonoBehaviour
    {
        private BasicInventory _inventory;

        private List<SlotView> _createdSlots;

        [Range(1, 8)]
        public int InitialSize;

        [Range(1, 8)]
        public int Columns;

        public ItemFactory ItemFactory;

        public GameObject SlotPrefab;

        public Transform SlotParent;

        public Slider SizeSlider;

        public event Action<BasicInventory> OnInventoryUpdate;

        public event Action<List<SlotView>> OnSizeChange;

        public int Size => _inventory.Size;

        private void Awake()
        {
            _inventory = new BasicInventory(InitialSize);
            _createdSlots = new List<SlotView>();

            ItemFactory.OnCollect += items => Collect(items);

            SizeSlider.onValueChanged.AddListener(f => Resize((int) f));

            OnInventoryUpdate += DrawSlots;

            OnInventoryUpdate(_inventory);

            OnSizeChange?.Invoke(_createdSlots);
        }

        public IItem Peek(int slot) => _inventory.Peek(slot);

        public (int, IItem[]) Add(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.Add(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate(_inventory);
            }

            return (count, remaining);
        }

        public (int, IItem[]) AddOrSwap(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.AddOrSwap(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate(_inventory);
            }

            return (count, remaining);
        }

        public (int, IItem[]) Collect(IItem[] items)
        {
            var (count, remaining) = _inventory.Collect(items);
            if (count > 0)
            {
                OnInventoryUpdate(_inventory);
            }

            return (count, remaining);
        }

        public IItem[] Remove(int count, int slot)
        {
            var items = _inventory.Remove(count, slot);
            if (items.Length > 0)
            {
                OnInventoryUpdate(_inventory);
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

            OnInventoryUpdate(_inventory);
        }

        public void Sort()
        {
            _inventory.Sort();

            OnInventoryUpdate(_inventory);
        }

        public void Resize(int newSize)
        {
            var didResize = _inventory.Resize(newSize);
            if (didResize)
            {
                OnInventoryUpdate(_inventory);
                OnSizeChange(_createdSlots);
            }
        }

        private void DrawSlots(BasicInventory inventory)
        {
            for (var i = 0; i < _createdSlots.Count; i++)
            {
                var slot = _createdSlots[i];
                var active = i < inventory.Size;

                // enable necessary slots and disable extra slots
                slot.gameObject.SetActive(active);
                slot.enabled = active;
            }

            if (_createdSlots.Count < inventory.Size)
            {
                // create any newly-required slots
                for (var i = _createdSlots.Count; i < inventory.Size; i++)
                {
                    var slot = Instantiate(SlotPrefab, SlotParent).GetComponent<SlotView>();

                    slot.gameObject.name += $"_{i}";
                    slot.transform.localPosition += new Vector3(75 * (i % Columns), -75 * (i / Columns), 0);

                    slot.ItemFactory = ItemFactory;
                    slot.SlotIndex = i;

                    OnInventoryUpdate += slot.Inventory_OnInventoryUpdate;

                    slot.Inventory_OnInventoryUpdate(inventory);

                    _createdSlots.Add(slot);
                }
            }
        }
    }
}
