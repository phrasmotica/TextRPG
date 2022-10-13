using System;
using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Inventory;
using VideoGameExperiments.Items;
using Color = UnityEngine.Color;

namespace TextRPG
{
    public class SlotView : MonoBehaviour
    {
        private bool _mouseIsOver;

        public ItemFactory ItemFactory;

        public int SlotIndex;

        public SpriteRenderer ItemPreview;

        public Text ItemAmountText;

        public Text SlotIndexText;

        public event Action<int> OnLeftClick;

        public event Action<int> OnRightClick;

        public bool ClickEventsRegistered { get; set; }

        private void OnMouseEnter() => _mouseIsOver = true;

        private void OnMouseExit() => _mouseIsOver = false;

        private void Awake()
        {
            SlotIndexText.text = $"{SlotIndex}";
        }

        private void Update()
        {
            if (_mouseIsOver)
            {
                // left click
                if (Input.GetMouseButtonUp(0))
                {
                    OnLeftClick?.Invoke(SlotIndex);
                }

                // right click
                if (Input.GetMouseButtonUp(1))
                {
                    OnRightClick?.Invoke(SlotIndex);
                }
            }
        }

        public void Inventory_OnInventoryUpdate(BasicInventory inventory)
        {
            SlotIndexText.text = $"{SlotIndex + 1}";

            var active = SlotIndex < inventory.Size;
            if (active)
            {
                var count = inventory.GetCount(SlotIndex);
                if (count > 0)
                {
                    ShowItem(inventory.Peek(SlotIndex));
                }
                else
                {
                    HideItem();
                }

                ItemAmountText.enabled = count > 1;
                ItemAmountText.text = $"x{count}";
                ItemAmountText.color = inventory.IsFull(SlotIndex) ? Color.red : Color.black;
            }
        }

        private void ShowItem(IItem item)
        {
            ItemPreview.enabled = true;
            ItemPreview.sprite = ItemFactory.GetSprite(item);
        }

        private void HideItem()
        {
            ItemPreview.enabled = false;
            ItemPreview.sprite = null;

            ItemAmountText.enabled = false;
        }
    }
}
