using DG.Tweening;
using System;
using UnityEngine;

namespace AtoLib.UI
{
    public class DOTweenLocalRotate : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;
        [SerializeField] private RotateMode mode = RotateMode.Fast;

        private void Reset()
        {
            target = transform;
        }

        public override void ResetState()
        {
            target.localRotation = Quaternion.Euler(from);
        }


        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOLocalRotate(to, Duration, mode);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private Vector3 preRotate;

        public override void Save()
        {
            preRotate = target.localRotation.eulerAngles;
        }

        public override void Load()
        {
            target.localRotation = Quaternion.Euler(preRotate);
        }


        [ContextMenu("Set Form")]
        private void SetStartState()
        {
            from = target.localRotation.eulerAngles;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
        {
            to = target.localRotation.eulerAngles;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localRotation = Quaternion.Euler(from);
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.localRotation = Quaternion.Euler(to);
        }
#endif
    }
}