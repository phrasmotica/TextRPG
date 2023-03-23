﻿using TMPro;
using UnityEngine;

namespace TextRPG
{
    public class DiceRollDescriptionText : MonoBehaviour
    {
        public TMP_Text Text;

        public DiceRollScreen DiceRollScreen;

        private void Awake()
        {
            SetText();
        }

        private void SetText()
        {
            var percentage = (int) (100 * DiceRollScreen.GetSuccessChance());
            Text.SetText($"Change of success: {percentage}%");
        }
    }
}
