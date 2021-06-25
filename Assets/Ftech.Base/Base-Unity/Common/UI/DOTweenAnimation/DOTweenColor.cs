using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class DOTweenColor : DOTweenTransition
    {
        [SerializeField] private Graphic target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private Color from = Color.white;
        [SerializeField] private Color to = Color.black;

        public Graphic Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public Color From { get => from; set => from = value; }
        public Color To { get => to; set => to = value; }

        private void Reset()
        {
            target = GetComponent<Graphic>();
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                target.color = from;
            }
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOColor(to, Duration);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private Color preColor;

        public override void Save()
        {
            preColor = target.color;
        }

        public override void Load()
        {
            target.color = preColor;
        }



        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.color;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.color;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget()
        {
            target.color = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.color = to;
        }
#endif
    }
}
