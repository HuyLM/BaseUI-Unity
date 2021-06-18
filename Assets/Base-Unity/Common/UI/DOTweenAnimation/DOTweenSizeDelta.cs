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
        private void SetStartState()
        {
            from = target.sizeDelta;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
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