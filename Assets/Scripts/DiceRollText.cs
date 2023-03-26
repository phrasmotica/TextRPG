using System.Collections;
using TextRPG.Extensions;
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

        public void Prime()
        {
            Text.color = Color.black;
        }

        public void SetRolling(WeightedDice dice)
        {
            _isRolling = true;
            StartCoroutine(Roll(dice));
        }

        private IEnumerator Roll(WeightedDice dice)
        {
            Text.color = Color.black.WithAlpha(0.3f);
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
    }
}
