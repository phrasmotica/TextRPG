using UnityEngine;

namespace TextRPG
{
    public class SpriteSwitcher : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        public void Switch(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
        }
    }
}
