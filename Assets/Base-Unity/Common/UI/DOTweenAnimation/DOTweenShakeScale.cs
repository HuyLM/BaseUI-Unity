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

        public Transform Target { get => target; set => target = value; }
        public Vector3 From { get => from; set => from = value; }
        public Vector3 Strength { get => strength; set => strength = value; }
        public int Vibrato { get => vibrato; set => vibrato = value; }
        public float Randomness { get => randomness; set => randomness = value; }
        public bool FadeOut { get => fadeOut; set => fadeOut = value; }

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
        public void SetFromState()
        {
            from = target.localScale;
        }
        [ContextMenu("Target => Form")]
        public void SetToState()
        {
            target.localScale = from;
        }
#endif
    }
}
