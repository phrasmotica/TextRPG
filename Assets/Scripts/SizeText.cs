using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace TextRPG
{
    public class SizeText : MonoBehaviour
    {
        public TMP_Text Text;

        public Slider SizeSlider;

        private void Awake()
        {
            SizeSlider.onValueChanged.AddListener(SetText);
        }

        public void SetText(float size)
        {
            Text.SetText($"Slots: {(int) size}");
        }
    }
}
