using UnityEngine;

namespace TextRPG.Inventory
{
    public class Cursor : MonoBehaviour
    {
        public HandView HandView;

        public SpriteRenderer CursorSprite;

        public Sprite HandOpenSprite;

        public Sprite HandClosedSprite;

        private void Awake()
        {
            HandView.OnPickupItems += UpdateHandView;
            HandView.OnPutBackItems += (_, remaining) => UpdateHandView(remaining.Length);
            HandView.OnClear += () => UpdateHandView(0);

            Show();
        }

        public void Show()
        {
            if (gameObject.activeInHierarchy)
            {
                CursorSprite.enabled = true;
                ShowNativeCursor(false);
            }
        }

        public void Hide()
        {
            if (gameObject.activeInHierarchy)
            {
                CursorSprite.enabled = false;
                ShowNativeCursor(true);
            }
        }

        private void ShowNativeCursor(bool show) => UnityEngine.Cursor.visible = show;

        private void UpdateHandView(int handCount)
        {
            CursorSprite.sprite = handCount > 0 ? HandClosedSprite : HandOpenSprite;
        }
    }
}
