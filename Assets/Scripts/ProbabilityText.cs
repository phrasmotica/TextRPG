using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class ProbabilityText : MonoBehaviour
    {
        public int Value;

        public bool Active;

        public TMP_Text ValueText;

        public TMP_Text ChanceText;

        public void SetText(int value, float chance, bool success)
        {
            Value = value;

            var colour = success ? Color.green : Color.red;

            if (!Active)
            {
                colour = WithAlpha(colour, 0.3f);
            }

            ValueText.SetText($"{value}");
            ValueText.color = colour;

            var percentage = (int) (100 * chance);

            ChanceText.SetText($"{percentage}%");
            ChanceText.color = colour;
        }

        public void SetActive(bool active)
        {
            Active = active;
            ValueText.color = WithAlpha(ValueText.color, active ? 1f : 0.3f);
            ChanceText.color = WithAlpha(ChanceText.color, active ? 1f : 0.3f);
        }

        private static Color WithAlpha(Color colour, float alpha)
        {
            return new Color(colour.r, colour.g, colour.b, alpha);
        }
    }
}
