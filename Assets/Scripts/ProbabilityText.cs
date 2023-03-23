using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class ProbabilityText : MonoBehaviour
    {
        public int Value;

        public bool Active;

        public DiceRollScreen DiceRollScreen;

        public TMP_Text ValueText;

        public TMP_Text ChanceText;

        private void Awake()
        {
            SetText();

            DiceRollScreen.OnReveal.AddListener((r, success) =>
            {
                Active = r == Value;
                SetText();
            });
        }

        private void SetText()
        {
            var colour = DiceRollScreen.IsSuccess(Value) ? Color.green : Color.red;

            if (!Active)
            {
                colour.a = 0.3f;
            }

            ValueText.SetText($"{Value}");
            ValueText.color = colour;

            var percentage = (int) (100 * DiceRollScreen.GetProbability(Value));

            ChanceText.SetText($"{percentage}%");
            ChanceText.color = colour;
        }
    }
}
