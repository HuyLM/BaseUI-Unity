using DG.Tweening;
using System;
using UnityEngine;

namespace Ftech.Lib.UI
{
    public class DOTweenAnchorPosition : DOTweenTransition
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private Vector2 from;
        [SerializeField] private Vector2 to;

        public RectTransform Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public Vector2 From { get => from; set => from = value; }
        public Vector2 To { get => to; set => to = value; }

        private void Reset()
        {
            target = transform as RectTransform;
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                target.anchoredPosition = from;
            }
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOAnchorPos(to, Duration);
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
        public void SetFromState()
        {
            from = target.anchoredPosition;
        }
        [ContextMenu("Set To")]
        public void SetToState()
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