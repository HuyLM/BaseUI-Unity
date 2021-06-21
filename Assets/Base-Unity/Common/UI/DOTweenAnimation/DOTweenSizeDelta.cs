using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI
{
    public class DOTweenSizeDelta : DOTweenTransition
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector2 from;
        [SerializeField] private Vector2 to;
        [SerializeField] private bool snapping = false;

        public RectTransform Target { get => target; set => target = value; }
        public Vector2 From { get => from; set => from = value; }
        public Vector2 To { get => to; set => to = value; }
        public bool Snapping { get => snapping; set => snapping = value; }


        private void Reset()
        {
            target = transform as RectTransform;
        }

        public override void ResetState()
        {
            target.sizeDelta = from;
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOSizeDelta(to, Duration, snapping);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private Vector3 preSize;

        public override void Save()
        {
            preSize = target.sizeDelta;
        }

        public override void Load()
        {
            target.sizeDelta = preSize;
        }




        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.sizeDelta;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.sizeDelta;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.sizeDelta = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.sizeDelta = to;
        }
#endif
    }
}