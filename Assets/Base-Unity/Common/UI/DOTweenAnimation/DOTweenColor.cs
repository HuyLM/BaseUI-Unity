using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    public class DOTweenColor : DOTweenTransition
    {
        [SerializeField] private Graphic target;
        [SerializeField] private Color from;
        [SerializeField] private Color to;

        public Graphic Target { get => target; set => target = value; }
        public Color From { get => from; set => from = value; }
        public Color To { get => to; set => to = value; }

        private void Reset()
        {
            target = GetComponent<Graphic>();
        }

        public override void ResetState()
        {
            target.color = from;
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
