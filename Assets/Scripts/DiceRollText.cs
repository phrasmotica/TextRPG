using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollText : MonoBehaviour
    {
        private string _initialText;

        public TMP_Text Text;

        private void Awake()
        {
            _initialText = Text.text;
        }

        public void SetRolling()
        {
            Text.SetText("...");
            Text.color = Color.black;
            Text.fontStyle = FontStyles.Normal;
        }

        public void SetResult(int result, bool success)
        {
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
