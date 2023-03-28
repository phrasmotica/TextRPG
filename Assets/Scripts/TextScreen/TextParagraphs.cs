using System.Collections.Generic;
using UnityEngine;

namespace TextRPG.TextScreen
{
    public class TextParagraphs : MonoBehaviour
    {
        [Range(0f, 3f)]
        public float DelaySeconds;

        public List<string> Paragraphs;
    }
}
