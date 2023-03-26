using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScrollbarManager : MonoBehaviour
    {
        public Scrollbar Scrollbar;

        private void Update()
        {
            Scrollbar.interactable = Scrollbar.size < 1;
        }

        public void ResetValue()
        {
            Scrollbar.value = 1f;
        }
    }
}
