using System.Linq;
using UnityEngine;

namespace TextRPG
{
    public class ActivateZone : MonoBehaviour
    {
        public int ItemId;

        public bool ClearHandOnActivate;

        public HandView HandView;

        public Clickable Clickable;

        private void Awake()
        {
            Clickable.CanClick = CanActivate;
        }

        public bool CanActivate()
        {
            var roll = Assets.Scripts.Dice.RollD6();
            var rollValue = roll.Values.Single().Value;

            Debug.Log($"You rolled a {rollValue}");

            return rollValue == 6 && HandView.Peek()?.Id == ItemId;
        }

        public void Highlight() => HandView.ShowHighlight();

        public void Normal() => HandView.ShowNormal();

        public void Clear()
        {
            if (ClearHandOnActivate)
            {
                HandView.Clear();
            }
        }

        public void ResetZone() => Clickable.HandleMouseExit();
    }
}
