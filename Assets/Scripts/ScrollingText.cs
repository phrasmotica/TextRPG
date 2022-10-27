using System;
using System.Collections;
using System.Linq;
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

        public bool CompressWhitespace;

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

                var waitTime = CharacterIntervalSeconds;

                if (charToAdd == ' ')
                {
                    if (CompressWhitespace)
                    {
                        // add a run of space characters in one go
                        var followingSpaces = _finalText.Skip(Text.text.Length).TakeWhile(c => c == ' ');
                        Text.text += string.Join(string.Empty, followingSpaces);
                    }

                    // wait a different amount of time for a run of space characters
                    waitTime = WhitespaceIntervalSeconds;
                }

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
