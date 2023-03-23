using System;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class DiceRollScreen : MonoBehaviour
    {
        private WeightedDice _dice;

        private int _rollValue;

        [Range(1, 6)]
        public int SuccessValue;

        public UnityEvent<int, bool> OnRoll;

        public UnityEvent OnSuccess;

        public UnityEvent OnFinish;

        private void Awake()
        {
            _dice = WeightedDice.Fair(1, 2, 3, 4, 5, 6);
        }

        public void Roll()
        {
            _rollValue = RollDice();
            var success = IsSuccess();

            Debug.Log($"You rolled {_rollValue}, success={success}");

            OnRoll?.Invoke(_rollValue, success);
        }

        public bool IsSuccess() => _rollValue == SuccessValue;

        public void ResetScreen()
        {
            _rollValue = 0;
        }

        public void Show(UnityAction onFinish, UnityAction onSuccess)
        {
            // TODO: allow re-rolling

            OnSuccess.AddListener(onSuccess);
            OnFinish.AddListener(onFinish);
        }

        public void Finish()
        {
            if (IsSuccess())
            {
                OnSuccess?.Invoke();
            }

            OnFinish?.Invoke();

            OnSuccess.RemoveAllListeners();
            OnFinish.RemoveAllListeners();

            ResetScreen();
        }

        public float GetSuccessChance() => _dice.GetProbability(SuccessValue);

        private int RollDice() => _dice.Roll(UnityEngine.Random.Range);
    }
}
