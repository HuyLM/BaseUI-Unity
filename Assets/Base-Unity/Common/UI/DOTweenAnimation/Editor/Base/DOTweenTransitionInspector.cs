using DG.Tweening;
using Ftech.Lib.UI;
using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenTransition))]
    public class DOTweenTransitionInspector : Editor
    {
        private bool isPlaying;
        protected DOTweenTransition transition;
        private SerializedProperty delayProperty;
        private SerializedProperty durationProperty;
        private SerializedProperty isSpeedBaseProperty;
        private SerializedProperty ignoreTimeScaleProperty;
        private SerializedProperty loopNumberProperty;
        private SerializedProperty loopTypeProperty;
        private SerializedProperty easeProperty;
        private SerializedProperty curveProperty;



        protected virtual void OnEnable()
        {
            isPlaying = false;
            transition = target as DOTweenTransition;

            delayProperty = serializedObject.FindProperty("delay");
            durationProperty = serializedObject.FindProperty("duration");
            isSpeedBaseProperty = serializedObject.FindProperty("isSpeedBase");
            ignoreTimeScaleProperty = serializedObject.FindProperty("ignoreTimeScale");
            loopNumberProperty = serializedObject.FindProperty("loopNumber");
            loopTypeProperty = serializedObject.FindProperty("loopType");
            easeProperty = serializedObject.FindProperty("ease");
            curveProperty = serializedObject.FindProperty("curve");

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
            EditorGUILayout.PropertyField(delayProperty);
            // Duration
            string label = transition.IsSpeedBase ? "Speed" : "Duration";
            EditorGUILayout.PropertyField(durationProperty, new GUIContent(label));
            // Is Speed Base
            EditorGUILayout.PropertyField(isSpeedBaseProperty);
            //  IgnoreTimeScale
            EditorGUILayout.PropertyField(ignoreTimeScaleProperty);

            // LoopNumber
            EditorGUILayout.PropertyField(loopNumberProperty);
            // LoopType
            if (transition.LoopNumber != 1 && transition.LoopNumber != 0)
            {
                EditorGUILayout.PropertyField(loopTypeProperty);
            }
            // Ease
            EditorGUILayout.PropertyField(easeProperty);
            // Curve
            if (transition.Ease == Ease.INTERNAL_Custom)
            {
                EditorGUILayout.PropertyField(curveProperty);
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
