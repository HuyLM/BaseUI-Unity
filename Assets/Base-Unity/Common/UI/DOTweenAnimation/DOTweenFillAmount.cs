using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI {
    public class DOTweenFillAmount : DOTweenTransition {
        [SerializeField] private Image target;
        [SerializeField] private float from;
        [SerializeField] private float to;

        private void Reset() {
            target = GetComponent<Image>();
        }

        public override void ResetState() {
            target.fillAmount = from;
        }

        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }
            
            Tween = target.DOFillAmount(to, Duration)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set From")]
        private void SetStartState() {
            from = target.color.a;
        }
        [ContextMenu("Set To")]
        private void SetFinishState() {
            to = target.color.a;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget() {
            target.fillAmount = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget() {
            target.fillAmount = to;
        }
#endif
    }
}
