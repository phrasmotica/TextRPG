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

        public bool CanActivate() => HandView.Peek()?.Id == ItemId;

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
