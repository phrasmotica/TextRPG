using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TextRPG
{
    public class PickupItemManager : MonoBehaviour
    {
        private Dictionary<PickupItem, bool> _enabledMap;

        private void Awake()
        {
            var pickupItems = GetComponentsInChildren<PickupItem>();

            // store initial enabled state of all pickup items
            _enabledMap = pickupItems.ToDictionary(i => i, i => i.gameObject.activeInHierarchy);
        }

        public void ResetItems()
        {
            foreach (var (item, enabled) in _enabledMap)
            {
                Debug.Log($"Resetting pickup item {item.gameObject.name} to enabled={enabled}");

                item.gameObject.SetActive(enabled);
                item.ResetItem();
            }
        }
    }
}
