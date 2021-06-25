using System;
using UnityEngine;
using DG.Tweening;

namespace Ftech.Lib.UI
{
    public class DOTweenScale : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;


        public Transform Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public Vector3 From { get => from; set => from = value; }
        public Vector3 To { get => to; set => to = value; }


        private void Reset()
        {
            target = transform;
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                target.localScale = from;
            }
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOScale(to, Duration);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private Vector3 preScale;

        public override void Save()
        {
            preScale = target.localScale;
        }

        public override void Load()
        {
            target.localScale = preScale;
        }



        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.localScale;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.localScale;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localScale = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.localScale = to;
        }
#endif
    }
}
