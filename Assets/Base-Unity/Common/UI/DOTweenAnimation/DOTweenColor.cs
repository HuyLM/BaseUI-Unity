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
        private void SetStartState()
        {
            from = target.color;
        }
        [ContextMenu("Set To")]
        private void SetFinishState()
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
