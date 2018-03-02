using UnityEditor;
using UnityEditor.UI;

namespace UnityToolbox
{
    /// <summary>
    /// Override UGUI ToggleEditor, it's use for TogglePlus.
    /// Inspired by: "http://www.tuicool.com/articles/AbyU7br"
    /// Author: BoJue.
    /// </summary>
    [CustomEditor(typeof(TogglePlus), true)]
    [CanEditMultipleObjects]
    public class TogglePlusEditor : ToggleEditor
    {
        SerializedProperty onImage;
        SerializedProperty offImage;

        protected override void OnEnable()
        {
            base.OnEnable();
            onImage = serializedObject.FindProperty("onImage");
            offImage = serializedObject.FindProperty("offImage");
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(onImage, true);
            EditorGUILayout.PropertyField(offImage, true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
