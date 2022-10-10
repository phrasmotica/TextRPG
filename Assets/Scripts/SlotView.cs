using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Inventory;

namespace TextRPG
{
    public class SlotView : MonoBehaviour
    {
        private bool _mouseIsOver;

        public Inventory Inventory;

        public int SlotIndex;

        public HandView HandView;

        public SpriteRenderer ItemPreview;

        public Text ItemAmountText;

        private void OnMouseEnter() => _mouseIsOver = true;
        private void OnMouseExit() => _mouseIsOver = false;

        private void Awake()
        {
            Inventory.OnInventoryUpdate += Inventory_OnInventoryUpdate;
        }

        private void Update()
        {
            if (_mouseIsOver)
            {
                // left click
                if (Input.GetMouseButtonUp(0))
                {
                    if (HandView.HasItems)
                    {
                        HandView.PutBackAll(SlotIndex);
                    }
                    else
                    {
                        HandView.PickUpAll(SlotIndex);
                    }
                }

                // right click
                if (Input.GetMouseButtonUp(1))
                {
                    var slotItem = Inventory.Peek(SlotIndex);

                    if (!HandView.HasItems)
                    {
                        HandView.PickUpHalf(SlotIndex);
                    }
                    else if (slotItem is null || HandView.Peek().CanStack(slotItem))
                    {
                        // put back one item if hand is not empty and stacking is possible
                        HandView.PutBackOne(SlotIndex);
                    }
                    else
                    {
                        Debug.Log($"Cannot put back {HandView.Peek().Name}, slot contains {slotItem.Name}(s)");
                    }
                }
            }
        }

        private void Inventory_OnInventoryUpdate(BasicInventory inventory)
        {
            var count = inventory.GetCount(SlotIndex);
            if (count > 0)
            {
                ItemPreview.enabled = true;
                ItemPreview.sprite = Inventory.GetSprite(inventory.Peek(SlotIndex));
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
