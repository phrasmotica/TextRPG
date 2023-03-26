using System.Collections.Generic;
using TextRPG.TextScreen;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class TextManager : MonoBehaviour
    {
        public List<string> InitialParagraphs;

        public GameObject TextScreenPrefab;

        private ScrollingText _currentText;

        public bool IsShowing { get; private set; }

        public UnityEvent OnShow;

        public UnityEvent OnHide;

        public void Begin()
        {
            ShowInitialTextScreen();
        }

        public void ShowTextScreen(List<string> paragraphs)
        {
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

        public void Skip() => _currentText.Skip();

        private void ShowInitialTextScreen() => ShowTextScreen(InitialParagraphs);
    }
}
