using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI {
    public class DOTweenFade : DOTweenTransition {
        [SerializeField] private CanvasGroup target;
        [SerializeField] private float from;
        [SerializeField] private float to;
        
        private void Reset() {
            target = GetComponent<CanvasGroup>();
        }

        public override void ResetState() {
            target.alpha = from;
        }
        
        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }

            Tween = target.DOFade(to, Duration)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set From")]
        private void SetStartState() {
            from = target.alpha;
        }
        [ContextMenu("Set To")]
        private void SetFinishState() {
            to = target.alpha;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget() {
            target.alpha = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget() {
            target.alpha = to;
        }
#endif
    }
}
