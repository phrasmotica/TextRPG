using System;
using TMPro;
using UnityEngine;

namespace TextRPG.TextScreen
{
    public class AutoResizeTextBox : MonoBehaviour
    {
        public TMP_Text Text;

        private RectTransform _textTransform;

        private Vector2 _initialSize;

        private bool _resizeNeeded;

        private void Awake()
        {
            _textTransform = Text.GetComponent<RectTransform>();
            _initialSize = _textTransform.sizeDelta;
        }

        private void Update()
        {
            if (_resizeNeeded)
            {
                _textTransform.sizeDelta = ComputeTextBoxSize();
                _resizeNeeded = false;
            }
        }

        public void ResizeNeeded()
        {
            _resizeNeeded = true;
        }

        private Vector2 ComputeTextBoxSize()
        {
            var currentSize = _textTransform.sizeDelta;

            var verticalMargins = Text.margin.z + Text.margin.w;
            var fittedHeight = Text.GetRenderedValues(false).y + verticalMargins;

            return new Vector2(currentSize.x, Math.Max(fittedHeight, _initialSize.y));
        }
    }
}
