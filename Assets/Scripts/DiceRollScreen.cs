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

        private int _attemptsLeft;

        public DiceType DiceType;

        [Range(1, 6)]
        public int SuccessValue;

        [Range(1, 3)]
        public int Attempts;

        public UnityEvent<WeightedDice, Func<int, bool>, int> OnCreate;

        public UnityEvent<WeightedDice> OnRoll;

        public UnityEvent<int, bool> OnReveal;

        public UnityEvent OnContinue;

        public UnityEvent OnSuccess;

        public UnityEvent OnFinish;

        private void Awake()
        {
            CreateDice();

            _attemptsLeft = Attempts;
        }

        private void CreateDice()
        {
            _dice = DiceType switch
            {
                DiceType.FairD6 => WeightedDice.FairD6(),
                DiceType.FavourableD6 => WeightedDice.FavourableD6(),
                _ => throw new InvalidOperationException(),
            };

            OnCreate?.Invoke(_dice, IsSuccess, Attempts);
        }

        public void Roll()
        {
            _attemptsLeft--;

            OnRoll?.Invoke(_dice);

            _rollValue = RollDice();
            var success = IsSuccess();

            Debug.Log($"You rolled {_rollValue}, success={success}");

            StartCoroutine(Reveal(_rollValue, success, _attemptsLeft));
        }

        private IEnumerator Reveal(int value, bool success, int rollsLeft)
        {
            yield return new WaitForSeconds(0.3f);

            OnReveal?.Invoke(value, success);

            if (success)
            {
                StartCoroutine(Finish(true));
            }
            else if (rollsLeft <= 0)
            {
                StartCoroutine(Finish(false));
            }
            else
            {
                StartCoroutine(Continue());
            }
        }

        private IEnumerator Continue()
        {
            yield return new WaitForSeconds(1f);

            OnContinue?.Invoke();
        }

        private IEnumerator Finish(bool success)
        {
            yield return new WaitForSeconds(1f);

            if (success)
            {
                OnSuccess?.Invoke();
            }

            OnFinish?.Invoke();

            OnContinue?.RemoveAllListeners();
            OnSuccess?.RemoveAllListeners();
            OnFinish?.RemoveAllListeners();

            ResetScreen();
        }

        public bool IsSuccess(int value) => SuccessValue == value;

        public bool IsSuccess() => IsSuccess(_rollValue);

        public void ResetScreen()
        {
            _rollValue = 0;
            _attemptsLeft = Attempts;
        }

        public float GetSuccessProbability() => GetProbability(SuccessValue);

        public float GetProbability(int value) => _dice.GetProbability(value);

        private int RollDice() => _dice.Roll(UnityEngine.Random.Range);
    }

    public enum DiceType
    {
        FairD6,
        FavourableD6,
    }
}
