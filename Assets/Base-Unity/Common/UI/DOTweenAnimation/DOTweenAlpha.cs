using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI {
    public class DOTweenAlpha : DOTweenTransition {
        [SerializeField] private Graphic target;
        [SerializeField] private float from;
        [SerializeField] private float to;

        private void Reset() {
            target = GetComponent<Graphic>();
        }

        public override void ResetState() {
            SetAlpha(target, from);
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

        private void SetAlpha(Graphic target, float alpha) {
            Color color = target.color;
            color.a = alpha;
            target.color = color;
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
            SetAlpha(target, from);
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget() {
            SetAlpha(target, to);
        }
#endif
    }
}
