﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace EnigmaCore.Timeline {
    [RequireComponent(typeof(Image), typeof(Canvas))]
    public class FadeController : MonoBehaviour {

        [SerializeField] Image _fadeImage;
        [SerializeField] Canvas _canvas;

        void OnValidate() {
            GetReferences();
        }

        void Reset() {
            GetReferences();
        }

        void Awake() {
            GetReferences();
        }

        void GetReferences() {
            TryGetComponent(out _fadeImage);
            TryGetComponent(out _canvas);
        }

        public void SetColor(Color c, float alpha) {
            _canvas.enabled = alpha > 0;
            c.a = alpha;
            _fadeImage.color = c;
        }

    }
}