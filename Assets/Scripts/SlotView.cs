using System;
using System.Collections.Generic;
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

        public HoverSprite HoverSprite;

        public int SlotIndex;

        public SpriteRenderer SlotSpriteRenderer;

        public SpriteRenderer ItemPreview;

        public Text ItemAmountText;

        public event Action<int> OnLeftClick;

        public event Action<int> OnRightClick;

        public bool ClickEventsRegistered { get; set; }

        private void OnMouseEnter() => _mouseIsOver = true;

        private void OnMouseExit() => _mouseIsOver = false;

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
            enabled = SlotIndex < Inventory.Size;

            if (enabled)
            {
                ShowEnabled();

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
            else
            {
                ShowDisabled();
                HideItem();
            }
        }

        private void ShowEnabled()
        {
            HoverSprite.enabled = true;
            SlotSpriteRenderer.color = Color.white;
        }

        private void ShowDisabled()
        {
            HoverSprite.enabled = false;
            SlotSpriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1);
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
