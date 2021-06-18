using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class DOTweenShakePosition : DOTweenTransition
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
            target.localPosition = from;
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOShakePosition(Duration, strength, vibrato, randomness, fadeOut);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private Vector2 prePosition;

        public override void Save()
        {
            prePosition = target.localPosition;
        }

        public override void Load()
        {
            target.localPosition = prePosition;
        }




        [ContextMenu("Set Form")]
        private void SetStartState()
        {
            from = target.localPosition;
        }
        [ContextMenu("Target => Form")]
        private void SetStartTarget()
        {
            target.localPosition = from;
        }
#endif
    }
}
