using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DiceRollButton : MonoBehaviour
    {
        public Button Button;

        public void Disable()
        {
            Button.interactable = false;
        }

        public void Enable()
        {
            Button.interactable = true;
        }
    }
}
