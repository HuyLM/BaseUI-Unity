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
        public void SetFromState()
        {
            from = target.localPosition;
        }
        [ContextMenu("Target => Form")]
        public void SetToState()
        {
            target.localPosition = from;
        }
#endif
    }
}
