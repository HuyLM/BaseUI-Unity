using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI {
    public class DOTweenShakePosition : DOTweenTransition {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 strength;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;
        [SerializeField] private bool fadeOut = false;

        private void Reset() {
            target = transform;
        }

        public override void ResetState() {
            target.localPosition = from;
        }

        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }
            Tween = target.DOShakePosition(Duration, strength, vibrato, randomness, fadeOut)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set Form")]
        private void SetStartState() {
            from = target.localPosition;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget() {
            target.localPosition = from;
        }
#endif
    }
}
