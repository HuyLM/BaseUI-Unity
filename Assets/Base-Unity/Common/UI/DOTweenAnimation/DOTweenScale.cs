using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI {
    public class DOTweenScale : DOTweenTransition {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;
        
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
            Tween = target.DOScale(to, Duration)
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
        [ContextMenu("Set To")]
        private void SetFinishState() {
            to = target.localScale;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget() {
            target.localScale = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget() {
            target.localScale = to;
        }
#endif
    }
}
