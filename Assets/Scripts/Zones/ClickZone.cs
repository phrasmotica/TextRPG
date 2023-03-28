using UnityEngine;
using UnityEngine.Events;

namespace TextRPG.Zones
{
    public class ClickZone : MonoBehaviour
    {
        public UnityEvent OnClick;

        private void OnMouseDown()
        {
            if (IsClicked())
            {
                OnClick?.Invoke();
            }
        }

        /// <summary>
        /// Returns whether this click zone has the "topmost" collider that overlaps with the mouse position.
        /// </summary>
        private bool IsClicked()
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // camera is at z = -10, background at z = 5. Distance of 20 is sufficient
            var distance = Vector2.Distance(Camera.main.transform.position, gameObject.transform.position) * 2;

            var hit = Physics2D.Raycast(wp, Vector2.zero, distance);
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }
    }
}
