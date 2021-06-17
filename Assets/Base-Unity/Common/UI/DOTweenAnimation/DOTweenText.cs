using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI {
    public class DOTweenText : DOTweenTransition {
        [SerializeField] private Text target;
        [SerializeField] private string from;
        [SerializeField] private string to;
        [SerializeField] private bool richTextEnabled = true;
        [SerializeField] private ScrambleMode scrambleMode = ScrambleMode.None;
        [SerializeField] private string scrambleChars;

        private void Reset() {
            target = GetComponent<Text>();
            if (target) {
                richTextEnabled = target.supportRichText;
            }
        }

        public override void ResetState() {
            target.text = from;
        }

        public override void DoTransition(Action onCompleted, bool restart) {
            if (restart) {
                ResetState();
            }
            Tween = target.DOText(to, Duration, richTextEnabled, scrambleMode, scrambleChars)
                          .SetEase(Ease)
                          .SetUpdate(IgnoreTimeScale)
                          .SetDelay(Delay)
                          .OnComplete(() => onCompleted?.Invoke());
        }

#if UNITY_EDITOR
        [ContextMenu("Set From")]
        private void SetStartState() {
            from = target.text;
        }
        [ContextMenu("Set To")]
        private void SetFinishState() {
            to = target.text;
        }
        [ContextMenu("Target => From")]
        private void SetStartTarget() {
            target.text = from;
        }
        [ContextMenu("Target => To")]
        private void SetFinishTarget() {
            target.text = to;
        }
#endif
    }
}
