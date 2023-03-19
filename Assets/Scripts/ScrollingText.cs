using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public bool ClickToSkip;

        public Button ContinueButton;

        public AudioSource Audio;

        public event UnityAction OnFinish;

        public Clickable Clickable;

        private string _finalText;

        private Coroutine _scrollingCoroutine;

        private void Awake()
        {
            Begin();
        }

        public void Begin()
        {
            ContinueButton.gameObject.SetActive(false);

            Clickable.CanClick = () => ClickToSkip;

            _finalText = Text.text;
            Text.text = string.Empty;

            _scrollingCoroutine = StartCoroutine(ScrollText());
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

                if (Audio != null)
                {
                    Audio.Stop();

                    // decrease denominator (currently 5) for a larger pitch range
                    // TODO: add float field for adjusting the pitch range from Unity inspector
                    Audio.pitch = 1 + (float) (Random.value - 0.5) / 5;

                    Audio.Play();
                }

                yield return new WaitForSeconds(waitTime);
            }

            Finish();
        }

        public void Skip()
        {
            Text.text = _finalText;

            StopCoroutine(_scrollingCoroutine);

            Finish();
        }

        private void Finish()
        {
            ShowContinueButton();
            StopAudio();

            OnFinish?.Invoke();
        }

        private void ShowContinueButton()
        {
            ContinueButton.gameObject.SetActive(true);
        }

        private void StopAudio()
        {
            if (Audio != null)
            {
                Audio.Stop();
            }
        }
    }
}
