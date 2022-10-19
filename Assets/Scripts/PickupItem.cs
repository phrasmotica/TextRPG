﻿using System;
using UnityEngine;
using VideoGameExperiments.Items;

namespace TextRPG
{
    public class PickupItem : MonoBehaviour
    {
        private bool _mouseOver;

        private int _currentCount;

        public int ItemId;

        [Range(1, 3)]
        public int Count;

        public SpriteRenderer SpriteRenderer;

        public HandView HandView;

        public event Action OnEnter;

        public event Action OnExit;

        public event Action<IItem> OnPickup;

        private void Awake()
        {
            OnEnter += ShowHighlight;
            OnExit += ShowNormal;
            OnPickup += HandView.Add;
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
                if (_currentCount < Count)
                {
                    OnPickup(ItemFactory.CreateItem(ItemId));

                    if (++_currentCount >= Count)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }

        private void ShowNormal() => SpriteRenderer.color = Color.white;
        private void ShowHighlight() => SpriteRenderer.color = Color.red;
    }
}
