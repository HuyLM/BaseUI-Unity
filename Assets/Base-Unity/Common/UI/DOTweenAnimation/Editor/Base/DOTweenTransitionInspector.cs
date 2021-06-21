using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenTransition))]
    public class DOTweenTransitionInspector : Editor
    {
        private bool isPlaying;
        protected DOTweenTransition transition;


        protected virtual void OnEnable()
        {
            isPlaying = false;
            transition = target as DOTweenTransition;
        }

        protected virtual void OnDisable()
        {
            Stop();
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((DOTweenTransition)target), typeof(DOTweenTransition), false);
            GUI.enabled = true;
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(isPlaying);
            if (GUILayout.Button("play", GUILayout.Height(50)))
            {
                Play();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(!isPlaying);
            if (GUILayout.Button("Stop", GUILayout.Height(50)))
            {
                Stop();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            //////
            GUILayout.Space(20);
            // Delay
            transition.Delay = EditorGUILayout.FloatField("Delay", transition.Delay);
            // Duration
            transition.Duration = EditorGUILayout.FloatField("Duration", transition.Duration);
            //  IgnoreTimeScale
            transition.IgnoreTimeScale = EditorGUILayout.Toggle("Ignore Timescale", transition.IgnoreTimeScale);

            // LoopNumber
            int loopNumber = EditorGUILayout.DelayedIntField("Loop Number", transition.LoopNumber);
            if (loopNumber != 0)
            {
                transition.LoopNumber = loopNumber;
            }
            // LoopType
            if (transition.LoopNumber != 1)
            {
                transition.LoopType = (LoopType)EditorGUILayout.EnumPopup("Loop Type", transition.LoopType);
            }
            // Ease
            transition.Ease = (Ease)EditorGUILayout.EnumPopup("Ease", transition.Ease);
            // Curve
            if (transition.Ease == Ease.INTERNAL_Custom)
            {
                transition.Curve = EditorGUILayout.CurveField("Curve", transition.Curve);
            }
            GUILayout.Space(20);
        }

        private void Play()
        {
            isPlaying = true;
            OnPlay();
            transition.PlayPreview();
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(transition.Tween);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
        }

        private void Stop()
        {
            if (isPlaying)
            {
                isPlaying = false;
                transition.StopPreview();
                DG.DOTweenEditor.DOTweenEditorPreview.Stop();
                OnStop();
            }
        }

        protected virtual void OnPlay()
        {

        }

        protected virtual void OnStop()
        {

        }
    }
}
