using System;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollResults : MonoBehaviour
    {
        public GameObject ResultTextPrefab;

        private List<DiceRollText> _texts;

        public void GenerateResultsText(WeightedDice dice, Func<int, bool> isSuccess)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            _texts = new();

            var numSides = dice.Sides.Count;
            var textWidth = ResultTextPrefab.GetComponent<RectTransform>().sizeDelta.x;
            var totalWidth = numSides * textWidth;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(totalWidth, rectTransform.sizeDelta.y);

            for (var i = 0; i < numSides; i++)
            {
                var text = Instantiate(ResultTextPrefab, transform).GetComponent<DiceRollText>();
                _texts.Add(text);

                // ensure they are spaced correctly
                var posX = i * textWidth - totalWidth / 2;
                text.transform.localPosition = new(posX, 0);

                //var value = dice.Sides[i].Value;
                //text.SetText(value, dice.GetProbability(value), isSuccess(value));
            }
        }

        //public void Reveal(int value, bool success)
        //{
        //    foreach (var text in _texts)
        //    {
        //        text.SetActive(text.Value == value);
        //    }
        //}

        //public void ResetText()
        //{
        //    foreach (var text in _texts)
        //    {
        //        text.SetActive(false);
        //    }
        //}
    }
}
