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

        public Inventory Inventory;

        public int SlotIndex;

        public SpriteRenderer SlotSprite;

        public SpriteRenderer ItemPreview;

        public Text ItemAmountText;

        public event Action<int> OnLeftClick;

        public event Action<int> OnRightClick;

        private void OnMouseEnter() => _mouseIsOver = true;

        private void OnMouseExit() => _mouseIsOver = false;

        private void Awake()
        {
            enabled = SlotIndex < Inventory.Size;

            if (enabled)
            {
                ShowEnabled();

                Inventory.OnInventoryUpdate += Inventory_OnInventoryUpdate;
            }
            else
            {
                ShowDisabled();
                HideItem();
            }
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

        private void Inventory_OnInventoryUpdate(BasicInventory inventory)
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

        private void ShowEnabled()
        {
            SlotSprite.color = Color.white;
        }

        private void ShowDisabled()
        {
            SlotSprite.color = Color.gray;
        }

        private void ShowItem(IItem item)
        {
            ItemPreview.enabled = true;
            ItemPreview.sprite = Inventory.GetSprite(item);
        }

        private void HideItem()
        {
            ItemPreview.enabled = false;
            ItemPreview.sprite = null;

            ItemAmountText.enabled = false;
        }
    }
}
