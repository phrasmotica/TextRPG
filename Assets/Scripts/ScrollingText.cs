using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class ScrollingText : MonoBehaviour
    {
        public TMP_Text Text;

        [Range(0.02f, 0.1f)]
        public float CharacterIntervalSeconds;

        public Button ContinueButton;

        public event Action OnFinish;

        private string _finalText;

        private void Awake()
        {
            OnFinish += ShowContinueButton;

            Begin();
        }

        public void Begin()
        {
            ContinueButton.gameObject.SetActive(false);

            _finalText = Text.text;
            Text.text = string.Empty;

            StartCoroutine(ScrollText());
        }

        private IEnumerator ScrollText()
        {
            while (Text.text.Length < _finalText.Length)
            {
                Text.text += _finalText[Text.text.Length];
                yield return new WaitForSeconds(CharacterIntervalSeconds);
            }

            OnFinish();
        }

        private void ShowContinueButton()
        {
            ContinueButton.gameObject.SetActive(true);
        }
    }
}
