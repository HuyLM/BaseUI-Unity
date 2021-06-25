using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class DOTweenFillAmount : DOTweenTransition
    {
        [SerializeField] private Image target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private float from;
        [SerializeField] private float to;

        public Image Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public float From { get => from; set => from = value; }
        public float To { get => to; set => to = value; }

        private void Reset()
        {
            target = GetComponent<Image>();
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                target.fillAmount = from;
            }
        }


        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOFillAmount(to, Duration);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private float preFill;

        public override void Save()
        {
            preFill = target.fillAmount;
        }

        public override void Load()
        {
            target.fillAmount = preFill;
        }


        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.color.a;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.color.a;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget()
        {
            target.fillAmount = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.fillAmount = to;
        }
#endif
    }
}
