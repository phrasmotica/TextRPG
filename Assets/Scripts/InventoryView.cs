using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VideoGameExperiments.Items;

namespace TextRPG
{
    public class InventoryView : MonoBehaviour
    {
        private bool _mouseIsOver;

        private List<IItem> _heldItems;

        public Inventory Inventory;

        public event Action<int> OnPickupItems;

        public event Action<int, IItem[]> OnPutBackItems;

        private void Awake()
        {
            _heldItems = new List<IItem>();

            OnPickupItems += InventoryView_OnPickupItems;
            OnPutBackItems += InventoryView_OnPutBackItems;
        }

        private void OnMouseEnter() => _mouseIsOver = true;
        private void OnMouseExit() => _mouseIsOver = false;

        private void Update()
        {
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

                        OnPickupItems?.Invoke(_heldItems.Count);
                    }
                }

                // right click
                if (Input.GetMouseButtonUp(1))
                {
                    var newItem = Inventory.Peek(0);

                    if (!_heldItems.Any())
                    {
                        // pick up half of all items if hand is empty
                        var items = Inventory.RemoveHalf(0);
                        if (items.Any())
                        {
                            _heldItems.AddRange(items);
                        }

                        OnPickupItems?.Invoke(items.Length);
                    }
                    else if (_heldItems[0].CanStack(newItem))
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
                        Debug.Log($"Cannot pick up {newItem.Name}, hand contains {_heldItems[0].Name}(s)");
                    }
                }
            }
        }

        private void InventoryView_OnPickupItems(int count)
        {
            Debug.Log($"Picked up {count} item(s)");
        }

        private void InventoryView_OnPutBackItems(int count, IItem[] remaining)
        {
            Debug.Log($"Put back {count} item(s), hand contains {remaining.Length} item(s)");
        }
    }
}
