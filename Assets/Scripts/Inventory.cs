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
        private readonly Dictionary<KeyCode, int> _itemMap = new()
        {
            [KeyCode.Alpha1] = 1,
            [KeyCode.Alpha2] = 2,
            [KeyCode.Alpha3] = 3,
        };

        private BasicInventory _inventory;

        private List<SlotView> _createdSlots;

        [Range(1, 8)]
        public int InitialSize;

        [Range(1, 8)]
        public int Columns;

        public GameObject SlotPrefab;

        public Transform SlotParent;

        public List<Sprite> ItemSprites;

        public Slider SizeSlider;

        public event Action<BasicInventory, List<SlotView>> OnInventoryUpdate;

        public int Size => _inventory.Size;

        private void Awake()
        {
            _inventory = new BasicInventory(InitialSize);
            _createdSlots = new List<SlotView>();

            SizeSlider.onValueChanged.AddListener(f => Resize((int) f));

            OnInventoryUpdate += (i, _) => DrawSlots(i);

            OnInventoryUpdate(_inventory, _createdSlots);
        }

        private void Update()
        {
            CollectNewItems();
        }

        public IItem Peek(int slot) => _inventory.Peek(slot);

        public (int, IItem[]) Add(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.Add(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate(_inventory, _createdSlots);
            }

            return (count, remaining);
        }

        public (int, IItem[]) AddOrSwap(IItem[] items, int slot)
        {
            var (count, remaining) = _inventory.AddOrSwap(items, slot);
            if (count > 0)
            {
                OnInventoryUpdate(_inventory, _createdSlots);
            }

            return (count, remaining);
        }

        public IItem[] Remove(int count, int slot)
        {
            var items = _inventory.Remove(count, slot);
            if (items.Length > 0)
            {
                OnInventoryUpdate(_inventory, _createdSlots);
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

            OnInventoryUpdate(_inventory, _createdSlots);
        }

        public void Sort()
        {
            _inventory.Sort();

            OnInventoryUpdate(_inventory, _createdSlots);
        }

        public void Resize(int newSize)
        {
            var didResize = _inventory.Resize(newSize);
            if (didResize)
            {
                OnInventoryUpdate(_inventory, _createdSlots);
            }
        }

        public Sprite GetSprite(IItem item)
        {
            var spriteIndex = item.Id - 1;
            return ItemSprites[spriteIndex];
        }

        private void CollectNewItems()
        {
            var didChange = false;

            foreach (var (keyCode, id) in _itemMap)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    var item = CreateItem(id);

                    var newItems = new List<IItem>
                    {
                        item,
                    };

                    // hold shift to add an entire stack
                    var additionalCount = Input.GetKey(KeyCode.LeftShift) ? item.MaxStackSize - 1 : 0;

                    for (var i = 0; i < additionalCount; i++)
                    {
                        newItems.Add(CreateItem(id));
                    }

                    var (count, remaining) = _inventory.Collect(newItems.ToArray());

                    didChange |= count > 0;
                }
            }

            if (didChange)
            {
                OnInventoryUpdate(_inventory, _createdSlots);
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
                slot.enabled = i < inventory.Size;
            }

            if (_createdSlots.Count < inventory.Size)
            {
                // create any newly-required slots
                for (var i = _createdSlots.Count; i < inventory.Size; i++)
                {
                    var slot = Instantiate(SlotPrefab, SlotParent).GetComponent<SlotView>();

                    slot.gameObject.name += $"_{i}";
                    slot.transform.localPosition += new Vector3(75 * (i % Columns), -75 * (i / Columns), 0);

                    slot.Inventory = this;
                    slot.SlotIndex = i;

                    OnInventoryUpdate += (i, _) => slot.Inventory_OnInventoryUpdate(i);

                    slot.Inventory_OnInventoryUpdate(inventory);

                    _createdSlots.Add(slot);
                }
            }
        }

        private static IItem CreateItem(int id) => id switch
        {
            1 => new Potion(),
            2 => new Coin(),
            3 => new Sword(),
            _ => throw new NotImplementedException(),
        };
    }
}
