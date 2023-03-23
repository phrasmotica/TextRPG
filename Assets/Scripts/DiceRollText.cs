using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class DiceRollText : MonoBehaviour
    {
        public TMP_Text Text;

        public UnityEvent OnReveal;

        public UnityEvent OnFinish;

        private string _initialText;

        private Coroutine _setTextCoroutine;

        private void Awake()
        {
            _initialText = Text.text;
        }

        public void SetResult(int result, bool success)
        {
            if (_setTextCoroutine != null)
            {
                StopCoroutine(_setTextCoroutine);
            }

            _setTextCoroutine = StartCoroutine(SetText(result, success));
        }

        private IEnumerator SetText(int result, bool success)
        {
            Text.SetText("...");
            Text.color = Color.black;
            Text.fontStyle = FontStyles.Normal;

            yield return new WaitForSeconds(0.3f);

            Text.SetText($"{result}");
            Text.color = success ? Color.green : Color.red;
            Text.fontStyle = FontStyles.Bold;

            OnReveal?.Invoke();

            yield return new WaitForSeconds(1f);

            ResetText();

            OnFinish?.Invoke();
        }

        private void ResetText()
        {
            Text.SetText(_initialText);
            Text.color = Color.black;
            Text.fontStyle = FontStyles.Normal;
        }
    }
}
