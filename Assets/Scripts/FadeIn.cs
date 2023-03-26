using System.Collections;
using TextRPG.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TextRPG
{
    public class FadeIn : MonoBehaviour
    {
        [Range(0.5f, 3f)]
        public float Duration;

        public Image Image;

        public UnityEvent OnFinish;

        private void Awake()
        {
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            while (Image.color.a > 0)
            {
                var fadeAmount = Image.color.a - (Time.deltaTime / Duration);
                var newColour = Image.color.WithAlpha(fadeAmount);
                Image.color = newColour;
                yield return null;
            }

            OnFinish?.Invoke();
        }
    }
}
