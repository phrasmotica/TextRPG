using UnityEngine;

namespace TextRPG
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
        }

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
