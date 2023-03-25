using System.Collections;
using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollText : MonoBehaviour
    {
        private string _initialText;

        private bool _isRolling;

        public TMP_Text Text;

        private void Awake()
        {
            _initialText = Text.text;
        }

        public void SetRolling(WeightedDice dice)
        {
            _isRolling = true;
            StartCoroutine(Roll(dice));
        }

        private IEnumerator Roll(WeightedDice dice)
        {
            Text.color = WithAlpha(Color.black, 0.3f);
            Text.fontStyle = FontStyles.Normal;

            while (_isRolling)
            {
                // do a "fake" roll for the preview
                var result = dice.Roll(Random.Range);

                Text.SetText($"{result}");

                yield return new WaitForSeconds(0.05f);
            }
        }

        public void SetResult(int result, bool success)
        {
            _isRolling = false;

            Text.SetText($"{result}");
            Text.color = success ? Color.green : Color.red;
            Text.fontStyle = FontStyles.Bold;
        }

        public void ResetText()
        {
            Text.SetText(_initialText);
            Text.color = Color.black;
            Text.fontStyle = FontStyles.Normal;
        }

        private static Color WithAlpha(Color colour, float alpha)
        {
            // TODO: create extension method for this
            return new Color(colour.r, colour.g, colour.b, alpha);
        }
    }
}
