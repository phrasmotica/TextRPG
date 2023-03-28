using System.Collections;
using System.Collections.Generic;
using TextRPG.TextScreen;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class TextManager : MonoBehaviour
    {
        private ScrollingText _currentText;

        public List<string> InitialParagraphs;

        public GameObject TextScreenPrefab;

        public UnityEvent<float> OnDelay;

        public UnityEvent OnShow;

        public UnityEvent OnHide;

        public bool IsShowing { get; private set; }

        public void Begin()
        {
            ShowInitialTextScreen();
        }

        public void ShowTextScreen(string text) => ShowTextScreen(new List<string> { text }, 0);

        public void ShowTextScreen(List<string> paragraphs) => ShowTextScreen(paragraphs, 0);

        public void ShowTextScreen(TextParagraphs text) => ShowTextScreen(text.Paragraphs, text.DelaySeconds);

        public void ShowTextScreen(List<string> paragraphs, float delaySeconds)
        {
            StartCoroutine(ShowParagraphs(paragraphs, delaySeconds));
        }

        public void Skip() => _currentText.Skip();

        private void ShowInitialTextScreen() => ShowTextScreen(InitialParagraphs);

        private IEnumerator ShowParagraphs(List<string> paragraphs, float delaySeconds)
        {
            if (delaySeconds > 0)
            {
                OnDelay?.Invoke(delaySeconds);

                yield return new WaitForSeconds(delaySeconds);
            }

            OnShow?.Invoke();

            IsShowing = true;

            _currentText = Instantiate(TextScreenPrefab, transform).GetComponent<ScrollingText>();

            _currentText.Paragraphs = paragraphs;

            _currentText.OnFinish.AddListener(() => IsShowing = false);
            _currentText.OnFinish.AddListener(() => Destroy(_currentText.gameObject));
            _currentText.OnFinish.AddListener(() => _currentText = null);
            _currentText.OnFinish.AddListener(() => OnHide?.Invoke());

            _currentText.Begin();
        }
    }
}
