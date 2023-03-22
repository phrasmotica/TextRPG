using Dice;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class DiceRollScreen : MonoBehaviour
    {
        private int _rollValue;

        public UnityEvent<int, bool> OnRoll;

        public void Roll()
        {
            _rollValue = RollD6();

            Debug.Log($"You rolled {_rollValue}");

            OnRoll?.Invoke(_rollValue, IsSuccess());
        }

        public bool IsSuccess() => _rollValue == 6;

        public void ResetScreen()
        {
            _rollValue = 0;
        }

        private static int RollD6()
        {
            var roll = Roller.Roll("1d6");
            return (int) roll.Values.Single().Value;
        }
    }
}
