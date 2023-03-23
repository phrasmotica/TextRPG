using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class ScreenManager : MonoBehaviour
    {
        private Dictionary<GameObject, bool> _enabledMap;

        public Cursor Cursor;

        public ScrollingText TextScreen;

        public PickupItemManager PickupItemManager;

        public ZoneManager ZoneManager;

        public GameObject DiceRollScreenPrefab;

        public GameObject GameWonScreenPrefab;

        private void Awake()
        {
            _enabledMap = new Dictionary<GameObject, bool>();

            foreach (Transform t in transform)
            {
                // store initial enabled state of all screens
                _enabledMap.Add(t.gameObject, t.gameObject.activeInHierarchy);
            }
        }

        public void ShowGameWonScreen()
        {
            var screen = Instantiate(GameWonScreenPrefab, transform).GetComponent<GameWonScreen>();

            screen.OnFinish.AddListener(PickupItemManager.ResetItems);
            screen.OnFinish.AddListener(ZoneManager.ResetZones);
            screen.OnFinish.AddListener(Cursor.Hide);
            screen.OnFinish.AddListener(ResetScreens);
            screen.OnFinish.AddListener(() => Destroy(screen.gameObject));
        }

        public void ShowDiceRollScreen(UnityAction onSuccess, UnityAction onFinish)
        {
            var screen = Instantiate(DiceRollScreenPrefab, transform).GetComponent<DiceRollScreen>();

            screen.OnSuccess.AddListener(onSuccess);

            screen.OnFinish.AddListener(onFinish);
            screen.OnFinish.AddListener(() => Destroy(screen.gameObject));
        }

        public void ResetScreens()
        {
            foreach (var (screen, enabled) in _enabledMap)
            {
                Debug.Log($"Resetting screen {screen.name} to enabled={enabled}");

                screen.SetActive(enabled);
            }

            TextScreen.Begin();
        }
    }
}
