using Dice;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class DiceRollScreen : MonoBehaviour
    {
        private int _rollValue;

        [Range(1, 6)]
        public int SuccessValue;

        public UnityEvent<int, bool> OnRoll;

        public UnityEvent OnSuccess;

        public UnityEvent OnFinish;

        public void Roll()
        {
            _rollValue = RollD6();
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
            gameObject.SetActive(true);

            OnFinish.AddListener(onFinish);
            OnSuccess.AddListener(onSuccess);
        }

        public void Finish()
        {
            gameObject.SetActive(false);

            OnFinish?.Invoke();

            if (IsSuccess())
            {
                OnSuccess?.Invoke();
            }

            ResetScreen();

            OnFinish.RemoveAllListeners();
            OnSuccess.RemoveAllListeners();
        }

        private static int RollD6()
        {
            var roll = Roller.Roll("1d6");
            return (int) roll.Values.Single().Value;
        }
    }
}
