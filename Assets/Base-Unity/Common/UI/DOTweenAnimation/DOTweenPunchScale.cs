using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenPunchScale : DOTweenTransition
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
            target.localScale = from;
        }


        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOPunchScale(punch, Duration, vibrato, elasticity);
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
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localScale = from;
        }
#endif
    }
}
