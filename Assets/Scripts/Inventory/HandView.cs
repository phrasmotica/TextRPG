using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Items;

namespace TextRPG.Inventory
{
    public class HandView : MonoBehaviour
    {
        private List<IItem> _heldItems;

        private bool _isPaused;

        public Inventory Inventory;

        public ItemFactory ItemFactory;

        public SpriteRenderer HandItemPreview;

        public Text HandItemAmountText;

        public event Action<int> OnPickupItems;

        public event Action<int, IItem[]> OnPutBackItems;

        public event Action OnClear;

        public bool HasItems => _heldItems.Any();

        private void Awake()
        {
            _heldItems = new List<IItem>();

            OnPickupItems += InventoryView_OnPickupItems;
            OnPutBackItems += InventoryView_OnPutBackItems;

            Inventory.OnSizeChange += slots =>
            {
                foreach (var slot in slots.Where(s => !s.ClickEventsRegistered))
                {
                    slot.OnLeftClick += Slot_OnLeftClick;
                    slot.OnRightClick += Slot_OnRightClick;

                    slot.ClickEventsRegistered = true;
                }
            };

            // ensure hand view shows as empty
            OnPickupItems(0);
        }

        private void Slot_OnLeftClick(int slotIndex)
        {
            if (HasItems)
            {
                PutBackAll(slotIndex);
            }
            else
            {
                PickUpAll(slotIndex);
            }
        }

        private void Slot_OnRightClick(int slotIndex)
        {
            var slotItem = Inventory.Peek(slotIndex);

            if (!HasItems)
            {
                PickUpHalf(slotIndex);
            }
            else if (slotItem is null || Peek().Id == slotItem.Id)
            {
                PutBackOne(slotIndex);
            }
            else
            {
                Debug.Log($"Cannot put back {Peek().Name}, slot contains {slotItem.Name}(s)");
            }
        }

        private void Update()
        {
            if (!_isPaused)
            {
                FollowMouse(transform);
            }
        }

        public void Add(IItem item)
        {
            _heldItems.Add(item);

            OnPickupItems(_heldItems.Count);
        }

        public IItem Peek() => HasItems ? _heldItems[0] : null;

        public void PickUpAll(int slot)
        {
            _heldItems = Inventory.RemoveAll(slot).ToList();

            OnPickupItems(_heldItems.Count);
        }

        public void PutBackAll(int slot)
        {
            var (count, remaining) = Inventory.AddOrSwap(_heldItems.ToArray(), slot);

            _heldItems = remaining.ToList();

            OnPutBackItems(count, remaining);
        }

        public void PickUpHalf(int slot)
        {
            var items = Inventory.RemoveHalf(slot);
            if (items.Any())
            {
                _heldItems.AddRange(items);
            }

            OnPickupItems(items.Length);
        }

        public void PutBackOne(int slot)
        {
            var (count, remaining) = Inventory.Add(_heldItems.Take(1).ToArray(), slot);
            if (count > 0)
            {
                _heldItems = _heldItems.Skip(count).Concat(remaining).ToList();
            }

            OnPutBackItems(count, _heldItems.ToArray());
        }

        public void Clear()
        {
            _heldItems.Clear();

            UpdateHandView();

            OnClear?.Invoke();
        }

        public void ShowNormal()
        {
            HandItemPreview.color = Color.white;
        }

        public void ShowHighlight()
        {
            HandItemPreview.color = Color.red;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        private static void FollowMouse(Transform t)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            t.position = mousePosition;
        }

        private void InventoryView_OnPickupItems(int count)
        {
            Debug.Log($"Picked up {count} item(s)");
            UpdateHandView();
        }

        private void InventoryView_OnPutBackItems(int count, IItem[] remaining)
        {
            Debug.Log($"Put back {count} item(s), hand contains {remaining.Length} item(s)");
            UpdateHandView();
        }

        private void UpdateHandView()
        {
            var handCount = _heldItems.Count;
            if (handCount > 0)
            {
                HandItemPreview.enabled = true;
                HandItemPreview.sprite = ItemFactory.GetSprite(_heldItems[0]);

                HandItemAmountText.color = handCount >= _heldItems[0].MaxStackSize ? Color.red : Color.black;
            }
            else
            {
                HandItemPreview.enabled = false;
                HandItemPreview.sprite = null;
            }

            HandItemAmountText.enabled = handCount > 1;
            HandItemAmountText.text = $"x{handCount}";
        }
    }
}
