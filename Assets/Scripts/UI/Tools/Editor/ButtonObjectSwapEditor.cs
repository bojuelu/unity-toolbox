using UnityEditor;
using UnityEditor.UI;

namespace UnityToolbox
{
    /// <summary>
    /// Button object swap editor.
    /// Author: BoJue.
    /// </summary>
    [CustomEditor(typeof(ButtonObjectSwap), true)]
    [CanEditMultipleObjects]
    public class ButtonObjectSwapEditor : ButtonEditor
    {
        SerializedProperty normal;
        SerializedProperty highlighted;
        SerializedProperty pressed;
        SerializedProperty disabled;

        protected override void OnEnable()
        {
        	base.OnEnable();
        	normal = serializedObject.FindProperty("normal");
        	highlighted = serializedObject.FindProperty("highlighted");
            pressed = serializedObject.FindProperty("pressed");
            disabled = serializedObject.FindProperty("disabled");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(normal);
            EditorGUILayout.PropertyField(highlighted);
            EditorGUILayout.PropertyField(pressed);
            EditorGUILayout.PropertyField(disabled);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
