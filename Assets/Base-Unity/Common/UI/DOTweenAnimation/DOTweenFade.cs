using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI
{
    public class DOTweenFade : DOTweenTransition
    {
        [SerializeField] private CanvasGroup target;
        [SerializeField] private float from;
        [SerializeField] private float to;

        public CanvasGroup Target { get => target; set => target = value; }
        public float From { get => from; set => from = value; }
        public float To { get => to; set => to = value; }

        private void Reset()
        {
            target = GetComponent<CanvasGroup>();
        }

        public override void ResetState()
        {
            target.alpha = from;
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOFade(to, Duration);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private float preAlpha;

        public override void Save()
        {
            preAlpha = target.alpha;
        }

        public override void Load()
        {
            target.alpha = preAlpha;
        }


        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.alpha;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.alpha;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget()
        {
            target.alpha = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.alpha = to;
        }
#endif
    }
}
