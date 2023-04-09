using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG.TextScreen
{
    public class TextParagraphs : MonoBehaviour
    {
        [Range(0f, 3f)]
        public float DelaySeconds;

        public List<string> Paragraphs;

        public UnityEvent OnFinish;

        public void Finish() => OnFinish?.Invoke();
    }
}
