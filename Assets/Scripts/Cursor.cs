using UnityEngine;

namespace TextRPG
{
    public class Cursor : MonoBehaviour
    {
        public HandView HandView;

        public ActivateZone GameWonZone;

        public SpriteRenderer CursorSprite;

        public Sprite HandOpenSprite;

        public Sprite HandClosedSprite;

        private void Awake()
        {
            HandView.OnPickupItems += UpdateHandView;
            HandView.OnPutBackItems += (_, remaining) => UpdateHandView(remaining.Length);
            HandView.OnClear += () => UpdateHandView(0);

            GameWonZone.OnActivate += Hide;

            Show();
        }

        public void Show()
        {
            CursorSprite.enabled = true;
            ShowNativeCursor(false);
        }

        private void Hide()
        {
            CursorSprite.enabled = false;
            ShowNativeCursor(true);
        }

        private void ShowNativeCursor(bool show) => UnityEngine.Cursor.visible = show;

        private void UpdateHandView(int handCount)
        {
            if (handCount > 0)
            {
                CursorSprite.sprite = HandClosedSprite;
            }
            else
            {
                CursorSprite.sprite = HandOpenSprite;
            }
        }
    }
}
