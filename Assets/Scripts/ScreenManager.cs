using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class ScreenManager : MonoBehaviour
    {
        private Dictionary<GameObject, bool> _enabledMap;

        public ScrollingText TextScreen;

        public GameObject GameWonScreen;

        public DiceRollScreen DiceRollScreen;

        private void Awake()
        {
            _enabledMap = new Dictionary<GameObject, bool>();

            foreach (Transform t in transform)
            {
                // store initial enabled state of all screens
                _enabledMap.Add(t.gameObject, t.gameObject.activeInHierarchy);
            }
        }

        public void ShowGameWonScreen() => GameWonScreen.SetActive(true);

        public void ShowDiceRollScreen(UnityAction onFinish, UnityAction onSuccess)
        {
            DiceRollScreen.Show(onFinish, onSuccess);
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
