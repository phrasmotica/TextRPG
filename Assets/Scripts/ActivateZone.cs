﻿using System;
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
        }

        private void OnMouseEnter()
        {
            _mouseOver = true;
            OnEnter();
        }

        private void OnMouseExit() => HandleMouseExit();

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

        public void ResetZone()
        {
            HandleMouseExit();
        }

        private void HandleMouseExit()
        {
            _mouseOver = false;
            OnExit();
        }
    }
}