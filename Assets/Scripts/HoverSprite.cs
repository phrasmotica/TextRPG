using UnityEngine;

namespace TextRPG
{
    /// <summary>
    /// Sets a sprite based on whether the mouse cursor is over the object.
    /// </summary>
    public class HoverSprite : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        public Sprite MouseEnterSprite;

        public Sprite MouseExitSprite;

        private void OnMouseEnter()
        {
            SpriteRenderer.sprite = MouseEnterSprite;
        }

        private void OnMouseExit()
        {
            SpriteRenderer.sprite = MouseExitSprite;
        }
    }
}
