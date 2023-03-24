using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollText : MonoBehaviour
    {
        public TMP_Text Text;

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
    }
}
