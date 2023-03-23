using UnityEngine;

namespace TextRPG
{
    public class ActivateZone : MonoBehaviour
    {
        public int ItemId;

        public bool ClearHandOnActivate;

        public HandView HandView;

        public Clickable Clickable;

        public ScreenManager ScreenManager;

        public Cursor Cursor;

        private void Awake()
        {
            Clickable.CanClick = CanActivate;
        }

        public bool CanActivate() => HandView.Peek()?.Id == ItemId;

        public void Highlight() => HandView.ShowHighlight();

        public void Normal() => HandView.ShowNormal();

        public void ShowDiceRoll()
        {
            HandView.Pause();

            ScreenManager.ShowDiceRollScreen(
                () =>
                {
                    Clear();

                    ScreenManager.ShowGameWonScreen();
                    Cursor.Hide();
                },
                () =>
                {
                    HandView.Resume();
                });
        }

        public void Clear()
        {
            if (ClearHandOnActivate)
            {
                HandView.Clear();
            }
        }

        public void ResetZone() => Clickable.HandleMouseExit();
    }
}
