using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class DOTweenAlpha : DOTweenTransition
    {
        [SerializeField] private Graphic target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private float from;
        [SerializeField] private float to;

        public Graphic Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public float From { get => from; set => from = value; }
        public float To { get => to; set => to = value; }

        private void Reset()
        {
            target = GetComponent<Graphic>();
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                SetAlpha(target, From);
            }
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOFade(to, Duration);
            base.CreateTween(onCompleted);
        }

        private void SetAlpha(Graphic target, float alpha)
        {
            Color color = target.color;
            color.a = alpha;
            target.color = color;
        }

#if UNITY_EDITOR

        private float preAlpha;

        public override void Save()
        {
            preAlpha = target.color.a;
        }

        public override void Load()
        {
            SetAlpha(target, preAlpha);
        }



        [ContextMenu("Set From")]
        public void SetFromState()
        {
            From = target.color.a;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.color.a;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget()
        {
            SetAlpha(target, From);
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            SetAlpha(target, to);
        }
#endif
    }
}
