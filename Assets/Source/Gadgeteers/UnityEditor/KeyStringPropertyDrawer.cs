using UnityEditor;
using UnityEngine;

namespace Source.Gadgeteers.UnityEditor
{
    [CustomPropertyDrawer(typeof(KeyString))]
    public class KeyStringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_value"), label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }
    }
}