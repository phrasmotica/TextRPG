using System;
using UnityEngine;

namespace TextRPG
{
    public class ActivateZone : MonoBehaviour
    {
        private bool _mouseOver;

        public int ItemId;

        public bool ClearHandOnActivate;

        public HandView HandView;

        public event Action OnEnter;

        public event Action OnExit;

        public event Action OnActivate;

        private void Awake()
        {
            OnEnter += HandView.ShowHighlight;
            OnExit += HandView.ShowNormal;

            if (ClearHandOnActivate)
            {
                OnActivate += HandView.Clear;
            }

            // TODO: show "game won" screen on activate
        }

        private void OnMouseEnter()
        {
            _mouseOver = true;
            OnEnter();
        }

        private void OnMouseExit()
        {
            _mouseOver = false;
            OnExit();
        }

        private void Update()
        {
            if (_mouseOver && Input.GetMouseButtonUp(0))
            {
                if (HandView.Peek()?.Id == ItemId)
                {
                    OnActivate?.Invoke();
                }
            }
        }
    }
}
