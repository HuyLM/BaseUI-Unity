using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenLocalPosition : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;

        public Transform Target { get => target; set => target = value; }
        public Vector3 From { get => from; set => from = value; }
        public Vector3 To { get => to; set => to = value; }

        private void Reset()
        {
            target = transform as RectTransform;
        }

        public override void ResetState()
        {
            target.localPosition = from;
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOLocalMove(to, Duration);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR



        private Vector3 prePositon;

        public override void Save()
        {
            prePositon = target.localPosition;
        }

        public override void Load()
        {
            target.localPosition = prePositon;
        }


        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.localPosition;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.localPosition;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localPosition = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.localPosition = to;
        }
#endif
    }
}
