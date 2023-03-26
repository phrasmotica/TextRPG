using System;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG.Dice
{
    public class DiceRollResults : MonoBehaviour
    {
        public GameObject ResultTextPrefab;

        private List<DiceRollText> _texts;

        private WeightedDice _dice;

        private int _currentRoll;

        public void GenerateResultsText(WeightedDice dice, Func<int, bool> isSuccess, int attempts)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            _texts = new();
            _dice = dice;

            var textWidth = ResultTextPrefab.GetComponent<RectTransform>().sizeDelta.x;
            var totalWidth = attempts * textWidth;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(totalWidth, rectTransform.sizeDelta.y);

            for (var i = 0; i < attempts; i++)
            {
                var text = Instantiate(ResultTextPrefab, transform).GetComponent<DiceRollText>();
                _texts.Add(text);

                var posX = i * textWidth - totalWidth / 2;
                text.transform.localPosition = new(posX, 0);

                if (i == 0)
                {
                    text.Prime();
                }
            }
        }

        public void SetRolling()
        {
            if (_currentRoll < _texts.Count)
            {
                var text = _texts[_currentRoll];
                text.SetRolling(_dice);
            }
        }

        public void Reveal(int value, bool success)
        {
            if (_currentRoll < _texts.Count)
            {
                var text = _texts[_currentRoll];
                text.SetResult(value, success);
            }
        }

        public void Next()
        {
            if (_currentRoll < _texts.Count - 1)
            {
                _currentRoll++;
                var text = _texts[_currentRoll];
                text.Prime();
            }
        }

        public void ResetText()
        {
            for (var i = 0; i < _texts.Count; i++)
            {
                var text = _texts[i];
                text.ResetText();

                if (i == 0)
                {
                    text.Prime();
                }
            }

            _currentRoll = 0;
        }
    }
}
