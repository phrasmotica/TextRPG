using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class GameWonScreen : MonoBehaviour
    {
        public UnityEvent OnFinish;

        public void Finish()
        {
            OnFinish?.Invoke();
        }
    }
}
