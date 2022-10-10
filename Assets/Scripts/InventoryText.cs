using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VideoGameExperiments.Inventory;

namespace TextRPG
{
    public class InventoryText : MonoBehaviour
    {
        public Text Text;

        public Inventory Inventory;

        private void Awake()
        {
            Inventory.OnInventoryUpdate += Inventory_OnInventoryUpdate;
        }

        private void Inventory_OnInventoryUpdate(BasicInventory obj)
        {
            Text.text = Describe(obj);
        }

        private static string Describe(BasicInventory inventory) => string.Join("\n", inventory.Slots.Select(DescribeSlot));

        private static string DescribeSlot(ISlot slot)
        {
            var item = slot.Peek();

            if (item is null)
            {
                return "(empty)";
            }

            if (slot.IsFull)
            {
                return $"{item.Name} x{slot.Count} (full)";
            }

            return $"{item.Name} x{slot.Count}";
        }
    }
}
