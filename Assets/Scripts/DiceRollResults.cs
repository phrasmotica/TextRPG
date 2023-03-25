using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollResults : MonoBehaviour
    {
        public GameObject ResultTextPrefab;

        private List<DiceRollText> _texts;

        private DiceRollText _current;

        private void Awake()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            _texts = new();

            var textWidth = ResultTextPrefab.GetComponent<RectTransform>().sizeDelta.x;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(textWidth, rectTransform.sizeDelta.y);
        }

        public void AddRollText(WeightedDice dice)
        {
            var text = Instantiate(ResultTextPrefab, transform).GetComponent<DiceRollText>();
            _texts.Add(text);

            var textWidth = ResultTextPrefab.GetComponent<RectTransform>().sizeDelta.x;
            var totalWidth = _texts.Count * textWidth;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(totalWidth, rectTransform.sizeDelta.y);

            var posX = (_texts.Count - 1) * textWidth - totalWidth / 2;
            text.transform.localPosition = new(posX, 0);

            text.SetRolling(dice);
            _current = text;
        }

        public void Reveal(int value, bool success)
        {
            _current.SetResult(value, success);
        }

        public void ResetText()
        {
            foreach (var text in _texts)
            {
                text.ResetText();
            }

            _current = null;
        }
    }
}
