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

        public UnityAction DiceRollCallback;

        public UnityAction DiceRollSuccessCallback;

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

        public void ShowDiceRollScreen(UnityAction callback, UnityAction successCallback)
        {
            DiceRollScreen.gameObject.SetActive(true);
            DiceRollCallback = callback;
            DiceRollSuccessCallback = successCallback;
        }

        public void HideDiceRollScreen()
        {
            DiceRollScreen.gameObject.SetActive(false);

            DiceRollCallback?.Invoke();

            if (DiceRollScreen.IsSuccess())
            {
                DiceRollSuccessCallback?.Invoke();
            }

            DiceRollScreen.ResetScreen();
            DiceRollSuccessCallback = null;
        }

        public void HideTextScreen()
        {
            TextScreen.gameObject.SetActive(false);
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
