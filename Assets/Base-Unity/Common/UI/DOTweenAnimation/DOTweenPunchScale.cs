using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI {
    public class DOTweenPunchScale : DOTweenTransition {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 punch;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1f;

        private void Reset() {
            target = transform;
        }

        public override void ResetState() {
            target.localScale = from;
        }

        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }
            Tween = target.DOPunchScale(punch, Duration, vibrato, elasticity)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set Form")]
        private void SetStartState() {
            from = target.localScale;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget() {
            target.localScale = from;
        }
#endif
    }
}
