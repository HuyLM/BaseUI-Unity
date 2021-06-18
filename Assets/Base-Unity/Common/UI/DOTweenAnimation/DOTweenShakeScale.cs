using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenShakeScale : DOTweenTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 strength;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;
        [SerializeField] private bool fadeOut = false;

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
            Tween = target.DOShakeScale(Duration, strength, vibrato, randomness, fadeOut);
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
