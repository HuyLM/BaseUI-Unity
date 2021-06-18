using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenAnimation))]
    public class DOTweenAnimationInspector : Editor
    {
        private bool isPlaying;
        protected DOTweenAnimation animation;


        protected virtual void OnEnable()
        {
            isPlaying = false;
            animation = target as DOTweenAnimation;
        }

        protected virtual void OnDisable()
        {
            Stop();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Load"))
            {
                animation.LoadTransitions();
            }

            EditorGUI.BeginDisabledGroup(isPlaying);
            if (GUILayout.Button("play"))
            {
                Play();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(!isPlaying);
            if (GUILayout.Button("Stop"))
            {
                Stop();
            }
            EditorGUI.EndDisabledGroup();

            base.OnInspectorGUI();

        }

        private void Play()
        {
            isPlaying = true;

            foreach (var transition in animation.Transitions)
            {
                transition.PlayPreview();
                DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(transition.Tween);
            }
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
        }

        private void Stop()
        {
            if (isPlaying)
            {
                isPlaying = false;

                foreach (var transition in animation.Transitions)
                {
                    transition.StopPreview();
                }
                DG.DOTweenEditor.DOTweenEditorPreview.Stop();

            }
        }
    }
}
