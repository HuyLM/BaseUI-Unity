using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI
{
    public class DOTweenAnchorPosition : DOTweenTransition
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
            target.anchoredPosition = from;
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOAnchorPos(to, Duration, snapping);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR



        private Vector2 prePositon;

        public override void Save()
        {
            prePositon = target.anchoredPosition;
        }

        public override void Load()
        {
            target.anchoredPosition = prePositon;
        }


        [ContextMenu("Set From")]
        private void SetStartState()
        {
            from = target.anchoredPosition;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
        {
            to = target.anchoredPosition;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.anchoredPosition = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.anchoredPosition = to;
        }
#endif
    }
}