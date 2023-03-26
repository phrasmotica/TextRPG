using TextRPG.Extensions;
using TMPro;
using UnityEngine;

namespace TextRPG.Dice
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
                colour = colour.WithAlpha(0.3f);
            }

            ValueText.SetText($"{value}");
            ValueText.color = colour;

            var percentage = 100 * chance;

            ChanceText.SetText($"{percentage:F1}%");
            ChanceText.color = colour;
        }

        public void SetActive(bool active)
        {
            Active = active;
            ValueText.color = ValueText.color.WithAlpha(active ? 1f : 0.3f);
            ChanceText.color = ChanceText.color.WithAlpha(active ? 1f : 0.3f);
        }
    }
}
