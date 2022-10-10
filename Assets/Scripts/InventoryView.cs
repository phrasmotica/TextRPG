using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Inventory;
using VideoGameExperiments.Items;

namespace TextRPG
{
    public class InventoryView : MonoBehaviour
    {
        private bool _mouseIsOver;

        private List<IItem> _heldItems;

        public Inventory Inventory;

        public SpriteRenderer ItemPreview;

        public Text ItemAmountText;

        public SpriteRenderer HandItemPreview;

        public Text HandItemAmountText;

        public event Action<int> OnPickupItems;

        public event Action<int, IItem[]> OnPutBackItems;

        private void Awake()
        {
            _heldItems = new List<IItem>();

            OnPickupItems += InventoryView_OnPickupItems;
            OnPutBackItems += InventoryView_OnPutBackItems;

            // ensure hand view shows as empty
            OnPickupItems(_heldItems.Count);

            Inventory.OnInventoryUpdate += Inventory_OnInventoryUpdate;
        }

        private void OnMouseEnter() => _mouseIsOver = true;
        private void OnMouseExit() => _mouseIsOver = false;

        private void Update()
        {
            FollowMouse(HandItemPreview.transform);

            if (_mouseIsOver)
            {
                // left click
                if (Input.GetMouseButtonUp(0))
                {
                    if (_heldItems.Any())
                    {
                        // put all held items back
                        var (count, remaining) = Inventory.AddOrSwap(_heldItems.ToArray(), 0);

                        _heldItems = remaining.ToList();

                        OnPutBackItems?.Invoke(count, remaining);
                    }
                    else
                    {
                        // pick up all items
                        _heldItems = Inventory.RemoveAll(0).ToList();

                        OnPickupItems(_heldItems.Count);
                    }
                }

                // right click
                if (Input.GetMouseButtonUp(1))
                {
                    var slotItem = Inventory.Peek(0);

                    if (!_heldItems.Any())
                    {
                        // pick up half of all items if hand is empty
                        var items = Inventory.RemoveHalf(0);
                        if (items.Any())
                        {
                            _heldItems.AddRange(items);
                        }

                        OnPickupItems(items.Length);
                    }
                    else if (_heldItems[0].CanStack(slotItem))
                    {
                        // put back one item if hand is not empty and stacking is possible

                        // TODO: if slot is full, "remaining" will contain the former slot contents.
                        // Ensure "_heldItems" is assigned properly to account for this
                        var (count, remaining) = Inventory.AddOrSwap(_heldItems.Take(1).ToArray(), 0);
                        if (count > 0)
                        {
                            _heldItems = _heldItems.Skip(count).ToList();
                        }

                        // TODO: pass "_heldItems" rather than "remaining" once above is fixed
                        OnPutBackItems?.Invoke(count, remaining);
                    }
                    else
                    {
                        Debug.Log($"Cannot put back {_heldItems[0].Name}, slot contains {slotItem.Name}(s)");
                    }
                }
            }
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
                HandItemPreview.sprite = Inventory.GetSprite(_heldItems[0]);
            }
            else
            {
                HandItemPreview.enabled = false;
                HandItemPreview.sprite = null;
            }

            HandItemAmountText.enabled = handCount > 1;
            HandItemAmountText.text = $"x{handCount}";
        }

        private void Inventory_OnInventoryUpdate(BasicInventory inventory)
        {
            var count = inventory.GetCount(0);
            if (count > 0)
            {
                ItemPreview.enabled = true;
                ItemPreview.sprite = Inventory.GetSprite(inventory.Peek(0));
            }
            else
            {
                ItemPreview.enabled = false;
                ItemPreview.sprite = null;
            }

            ItemAmountText.enabled = count > 1;
            ItemAmountText.text = $"x{count}";
        }
    }
}
