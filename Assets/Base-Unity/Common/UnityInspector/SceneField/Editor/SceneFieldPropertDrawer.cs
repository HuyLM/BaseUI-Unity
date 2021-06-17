using UnityEngine;
using UnityEditor;

namespace GameSystem.Common.UnityInspector.Editor {
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))]
    public class SceneFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool isInteger = property.propertyType == SerializedPropertyType.Integer;
            bool isString = property.propertyType == SerializedPropertyType.String;

            if (isInteger) {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                GUIContent[] allSceneNameDisplay = new GUIContent[scenes.Length];

                for (int i = 0; i < scenes.Length; i++) {
                    string path = scenes[i].path;
                    string name = System.IO.Path.GetFileNameWithoutExtension(path);
                    GUIContent content = new GUIContent(string.Format("{0}. {1}", i, name));
                    allSceneNameDisplay[i] = content;
                }

                EditorGUI.BeginChangeCheck();

                int sceneIndex = property.intValue;
                sceneIndex = EditorGUI.Popup(position, label, sceneIndex, allSceneNameDisplay);

                if (EditorGUI.EndChangeCheck()) {
                    property.intValue = sceneIndex;
                }
            } else if (isString) {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                string[] allSceneName = new string[scenes.Length];
                GUIContent[] allSceneNameDisplay = new GUIContent[scenes.Length];
                string sceneName = property.stringValue;
                int sceneIndex = 0;

                for (int i = 0; i < scenes.Length; i++) {
                    string path = scenes[i].path;
                    string name = System.IO.Path.GetFileNameWithoutExtension(path);
                    allSceneName[i] = name;
                    GUIContent content = new GUIContent(string.Format("{0}. {1}", i, name));
                    allSceneNameDisplay[i] = content;

                    if (name == sceneName) {
                        sceneIndex = i;
                    }
                }

                EditorGUI.BeginChangeCheck();

                sceneIndex = EditorGUI.Popup(position, label, sceneIndex, allSceneNameDisplay);

                if (EditorGUI.EndChangeCheck()) {
                    property.stringValue = allSceneName[sceneIndex];
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}