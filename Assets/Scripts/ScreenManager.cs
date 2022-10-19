using UnityEngine;

namespace TextRPG
{
    public class ScreenManager : MonoBehaviour
    {
        public GameObject GameWonScreen;

        public ActivateZone GameWonZone;

        private void Awake()
        {
            GameWonZone.OnActivate += () => GameWonScreen.SetActive(true);
        }
    }
}
