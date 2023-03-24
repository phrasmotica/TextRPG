using System;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class DiceProbabilities : MonoBehaviour
    {
        public GameObject ProbabilitiesTextPrefab;

        private List<ProbabilityText> _texts;

        public void GenerateProbabilitiesText(WeightedDice dice, Func<int, bool> isSuccess)
        {
            _texts = new();

            var numSides = dice.Sides.Count;
            var textWidth = ProbabilitiesTextPrefab.GetComponent<RectTransform>().sizeDelta.x;
            var totalWidth = numSides * textWidth;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(totalWidth, rectTransform.sizeDelta.y);

            for (var i = 0; i < numSides; i++)
            {
                var text = Instantiate(ProbabilitiesTextPrefab, transform).GetComponent<ProbabilityText>();
                _texts.Add(text);

                // ensure they are spaced correctly
                var posX = i * textWidth - totalWidth / 2;
                text.transform.localPosition = new(posX, 0);

                var value = dice.Sides[i].Value;
                text.SetText(value, dice.GetProbability(value), isSuccess(value));
            }
        }

        public void Reveal(int value, bool success)
        {
            foreach (var text in _texts)
            {
                text.SetActive(text.Value == value);
            }
        }
    }
}
