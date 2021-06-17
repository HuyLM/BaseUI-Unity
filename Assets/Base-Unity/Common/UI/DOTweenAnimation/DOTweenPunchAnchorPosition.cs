using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI {
    public class DOTweenPunchAnchorPosition : DOTweenTransition {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector2 from;
        [SerializeField] private Vector2 punch;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1f;
        [SerializeField] private bool snapping = false;

        private void Reset() {
            target = transform as RectTransform;
        }

        public override void ResetState() {
            target.anchoredPosition = from;
        }

        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }

            Tween = target.DOPunchAnchorPos(punch, Duration, vibrato, elasticity, snapping)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set From")]
        private void SetStartState() {
            from = target.anchoredPosition;
        }

        [ContextMenu("Target => From")]
        private void SetStartTarget() {
            target.anchoredPosition = from;
        }
#endif
    }
}