using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    public class DOTweenFillAmount : DOTweenTransition
    {
        [SerializeField] private Image target;
        [SerializeField] private float from;
        [SerializeField] private float to;

        private void Reset()
        {
            target = GetComponent<Image>();
        }

        public override void ResetState()
        {
            target.fillAmount = from;
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
        private void SetStartState()
        {
            from = target.color.a;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
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
