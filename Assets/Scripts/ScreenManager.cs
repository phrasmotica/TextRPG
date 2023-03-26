using System.Collections.Generic;
using TextRPG.Dice;
using TextRPG.Inventory;
using TextRPG.TextScreen;
using TextRPG.Zones;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class ScreenManager : MonoBehaviour
    {
        public List<string> InitialParagraphs;

        public Inventory.Cursor Cursor;

        public PickupItemManager PickupItemManager;

        public ZoneManager ZoneManager;

        public GameObject TextScreenPrefab;

        public GameObject DiceRollScreenPrefab;

        public GameObject GameWonScreenPrefab;

        private void Awake()
        {
            ShowInitialTextScreen();
        }

        public void ShowInitialTextScreen()
        {
            var screen = Instantiate(TextScreenPrefab, transform).GetComponent<ScrollingText>();

            screen.Paragraphs = InitialParagraphs;
            screen.OnFinish.AddListener(() => Destroy(screen.gameObject));

            screen.Begin();
        }

        public void ShowGameWonScreen()
        {
            var screen = Instantiate(GameWonScreenPrefab, transform).GetComponent<GameWonScreen>();

            screen.OnFinish.AddListener(PickupItemManager.ResetItems);
            screen.OnFinish.AddListener(ZoneManager.ResetZones);
            screen.OnFinish.AddListener(Cursor.Hide);
            screen.OnFinish.AddListener(ShowInitialTextScreen);
            screen.OnFinish.AddListener(() => Destroy(screen.gameObject));
        }

        public void ShowDiceRollScreen(UnityAction onSuccess, UnityAction onFinish)
        {
            var screen = Instantiate(DiceRollScreenPrefab, transform).GetComponent<DiceRollScreen>();

            screen.OnSuccess.AddListener(onSuccess);

            screen.OnFinish.AddListener(onFinish);
            screen.OnFinish.AddListener(() => Destroy(screen.gameObject));
        }
    }
}
