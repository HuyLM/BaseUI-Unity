using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class DOTweenText : DOTweenTransition
    {
        [SerializeField] private Text target;
        [SerializeField] private bool fromCurrent;
        [SerializeField] private string from;
        [SerializeField] private string to;
        [SerializeField] private bool richTextEnabled = true;
        [SerializeField] private ScrambleMode scrambleMode = ScrambleMode.None;
        [SerializeField] private string scrambleChars;

        public Text Target { get => target; set => target = value; }
        public bool FromCurrent { get => fromCurrent; set => fromCurrent = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public bool RichTextEnabled { get => richTextEnabled; set => richTextEnabled = value; }
        public ScrambleMode ScrambleMode { get => scrambleMode; set => scrambleMode = value; }
        public string ScrambleChars { get => scrambleChars; set => scrambleChars = value; }


        private void Reset()
        {
            target = GetComponent<Text>();
            if (target)
            {
                richTextEnabled = target.supportRichText;
            }
        }

        public override void ResetState()
        {
            if (!fromCurrent)
            {
                target.text = from;
            }
        }

        public override void CreateTween(Action onCompleted)
        {
            Tween = target.DOText(to, Duration, richTextEnabled, scrambleMode, scrambleChars);
            base.CreateTween(onCompleted);
        }

#if UNITY_EDITOR

        private string preString;

        public override void Save()
        {
            preString = target.text;
        }

        public override void Load()
        {
            target.text = preString;
        }




        [ContextMenu("Set From")]
        public void SetFromState()
        {
            from = target.text;
        }
        [ContextMenu("Set To")]
        public void SetToState()
        {
            to = target.text;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget()
        {
            target.text = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget()
        {
            target.text = to;
        }
#endif
    }
}
