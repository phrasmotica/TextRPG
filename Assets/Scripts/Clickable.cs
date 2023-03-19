using System;
using UnityEngine;
using UnityEngine.Events;

namespace TextRPG
{
    public class Clickable : MonoBehaviour
    {
        private bool _mouseOver;

        public UnityEvent OnEnter;

        public UnityEvent OnExit;

        public UnityEvent OnClick;

        public Func<bool> CanClick = () => true;

        private void OnMouseEnter() => HandleMouseEnter();

        private void OnMouseExit() => HandleMouseExit();

        private void Update()
        {
            if (_mouseOver && CanClick() && Input.GetMouseButtonUp(0))
            {
                OnClick?.Invoke();
            }
        }

        public void HandleMouseEnter()
        {
            _mouseOver = true;
            OnEnter?.Invoke();
        }

        public void HandleMouseExit()
        {
            _mouseOver = false;
            OnExit?.Invoke();
        }
    }
}
