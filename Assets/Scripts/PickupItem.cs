using UnityEngine;

namespace TextRPG
{
    public class PickupItem : MonoBehaviour
    {
        private bool _mouseOver;

        [Range(1, 3)]
        public int Count;

        public ItemFactory ItemFactory;

        private int _currentCount;

        private void OnMouseEnter() => _mouseOver = true;

        private void OnMouseExit() => _mouseOver = false;

        private void Update()
        {
            if (_mouseOver && Input.GetMouseButtonUp(0))
            {
                if (_currentCount < Count)
                {
                    ItemFactory.Create(1);

                    if (++_currentCount >= Count)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
