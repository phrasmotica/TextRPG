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

        [Range(0.02f, 0.1f)]
        public float WhitespaceIntervalSeconds;

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
                var charToAdd = _finalText[Text.text.Length];
                Text.text += charToAdd;

                // wait a different amount of time for a space character
                // TODO: wait the same amount of time for any number of space characters...
                var waitTime = charToAdd == ' ' ? WhitespaceIntervalSeconds : CharacterIntervalSeconds;
                yield return new WaitForSeconds(waitTime);
            }

            OnFinish();
        }

        private void ShowContinueButton()
        {
            ContinueButton.gameObject.SetActive(true);
        }
    }
}
