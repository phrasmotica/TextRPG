using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace TextRPG.TextScreen
{
    public class ScrollingText : MonoBehaviour
    {
        public TMP_Text Text;

        public List<string> Paragraphs;

        [Range(0.02f, 0.1f)]
        public float CharacterIntervalSeconds;

        [Range(0.02f, 0.1f)]
        public float WhitespaceIntervalSeconds;

        public bool CompressWhitespace;

        public AudioSource Audio;

        public UnityEvent OnTextChange;

        public UnityEvent OnFinish;

        private int _currentIndex;

        private string _finalText;

        private bool _isScrolling;

        private Coroutine _scrollingCoroutine;

        public void Begin()
        {
            _currentIndex = 0;
            _finalText = string.Empty;
            _isScrolling = false;
            _scrollingCoroutine = null;

            if (Paragraphs.Any())
            {
                _scrollingCoroutine = StartCoroutine(ScrollText());
            }
        }

        private IEnumerator ScrollText()
        {
            StartParagraph();

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

                OnTextChange?.Invoke();

                if (Audio != null && Audio.enabled)
                {
                    Audio.Stop();

                    // decrease denominator (currently 5) for a larger pitch range
                    // TODO: add float field for adjusting the pitch range from Unity inspector
                    Audio.pitch = 1 + (float) (Random.value - 0.5) / 5;

                    Audio.Play();
                }

                yield return new WaitForSeconds(waitTime);
            }

            EndParagraph();
        }

        private void StartParagraph()
        {
            _isScrolling = true;
            _finalText = Paragraphs[_currentIndex];

            Text.text = string.Empty;
        }

        public void Skip()
        {
            if (_isScrolling)
            {
                StopCoroutine(_scrollingCoroutine);

                EndParagraph();
            }
            else if (_currentIndex < Paragraphs.Count - 1)
            {
                NextParagraph();
            }
            else
            {
                Finish();
            }
        }

        private void EndParagraph()
        {
            Text.text = _finalText;
            _isScrolling = false;

            OnTextChange?.Invoke();

            StopAudio();
        }

        private void NextParagraph()
        {
            _currentIndex++;
            _scrollingCoroutine = StartCoroutine(ScrollText());
        }

        private void StopAudio()
        {
            if (Audio != null)
            {
                Audio.Stop();
            }
        }

        private void Finish()
        {
            OnFinish?.Invoke();
        }
    }
}
