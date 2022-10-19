using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TextRPG
{
    public class ZoneManager : MonoBehaviour
    {
        private Dictionary<ActivateZone, bool> _enabledMap;

        private void Awake()
        {
            var pickupItems = GetComponentsInChildren<ActivateZone>();

            // store initial enabled state of all zones
            _enabledMap = pickupItems.ToDictionary(i => i, i => i.gameObject.activeInHierarchy);
        }

        public void ResetZones()
        {
            foreach (var (zone, enabled) in _enabledMap)
            {
                Debug.Log($"Resetting zone {zone.gameObject.name} to enabled={enabled}");

                zone.gameObject.SetActive(enabled);
                zone.ResetZone();
            }
        }
    }
}
