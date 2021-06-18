using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenScale : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;

        private void Reset()
        {
            target = transform;
        }

        public override void ResetState()
        {
            target.localScale = from;
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



        [ContextMenu("Set Form")]
        private void SetStartState()
        {
            from = target.localScale;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
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
