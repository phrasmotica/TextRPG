using System;
using System.Collections;
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

        public UnityEvent<int, bool> OnReveal;

        public UnityEvent OnSuccess;

        public UnityEvent OnFinish;

        private void Awake()
        {
            _dice = new WeightedDice(new()
            {
                new Side(1, 1),
                new Side(2, 1),
                new Side(3, 1),
                new Side(4, 1),
                new Side(5, 1),
                new Side(6, 5),
            });

            // TODO: instantiate ProbabilityText objects based on the sides of the die
        }

        public void Roll()
        {
            _rollValue = RollDice();
            var success = IsSuccess();

            Debug.Log($"You rolled {_rollValue}, success={success}");

            OnRoll?.Invoke(_rollValue, success);

            StartCoroutine(Reveal(_rollValue, success));
        }

        private IEnumerator Reveal(int value, bool success)
        {
            yield return new WaitForSeconds(0.3f);

            OnReveal?.Invoke(value, success);

            StartCoroutine(Finish());
        }

        private IEnumerator Finish()
        {
            yield return new WaitForSeconds(1f);

            if (IsSuccess())
            {
                OnSuccess?.Invoke();
            }

            OnFinish?.Invoke();

            OnSuccess.RemoveAllListeners();
            OnFinish.RemoveAllListeners();

            ResetScreen();
        }

        public bool IsSuccess(int value) => SuccessValue == value;

        public bool IsSuccess() => IsSuccess(_rollValue);

        public void ResetScreen()
        {
            _rollValue = 0;
        }

        public void Show(UnityAction onFinish, UnityAction onSuccess)
        {
            // TODO: allow re-rolling the die

            OnSuccess.AddListener(onSuccess);
            OnFinish.AddListener(onFinish);
        }

        public float GetSuccessProbability() => GetProbability(SuccessValue);

        public float GetProbability(int value) => _dice.GetProbability(value);

        private int RollDice() => _dice.Roll(UnityEngine.Random.Range);
    }
}
