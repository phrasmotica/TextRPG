using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Background : MonoBehaviour
    {
        public TextManager TextManager;

        public List<string> PromptParagraphs;

        public void Prompt()
        {
            if (!TextManager.IsShowing)
            {
                TextManager.ShowTextScreen(PromptParagraphs);
            }
            else
            {
                TextManager.Skip();
            }
        }

        public void SetPromptParagraphs(string paragraph)
        {
            PromptParagraphs = new List<string> { paragraph };
        }
    }
}
