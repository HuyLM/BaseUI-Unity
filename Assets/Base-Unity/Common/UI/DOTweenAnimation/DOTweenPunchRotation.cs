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

        public Transform Target { get => target; set => target = value; }
        public Vector3 From { get => from; set => from = value; }
        public Vector3 Punch { get => punch; set => punch = value; }
        public int Vibrato { get => vibrato; set => vibrato = value; }
        public float Elasticity { get => elasticity; set => elasticity = value; }


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
        public void SetFromState()
        {
            from = target.localRotation.eulerAngles;
        }
        [ContextMenu("Target => Form")]
        public void SetToState()
        {
            target.localRotation = Quaternion.Euler(from);
        }
#endif
    }
}
