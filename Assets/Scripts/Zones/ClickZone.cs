using UnityEngine;
using UnityEngine.Events;

namespace TextRPG.Zones
{
    public class ClickZone : MonoBehaviour
    {
        public UnityEvent OnClick;

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }
    }
}
