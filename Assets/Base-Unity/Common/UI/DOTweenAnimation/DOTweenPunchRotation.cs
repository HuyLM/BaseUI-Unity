using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenPunchRotation : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 punch;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1f;

        private void Reset()
        {
            target = transform;
        }

        public override void ResetState()
        {
            target.rotation = Quaternion.Euler(from);
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOPunchRotation(punch, Duration, vibrato, elasticity);
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
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localRotation = Quaternion.Euler(from);
        }
#endif
    }
}
