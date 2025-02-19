using System;
using UnityEditor;
using UnityEngine;

namespace Marx.Utilities
{

    [CustomPropertyDrawer(typeof(SGuid))]
    public class SGuidDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect guidFieldRect = new Rect(position)
            {
                width = position.width - 50
            };
            Rect generateButtonRect = position.ShiftAndResizeX(guidFieldRect.width);

            SerializedProperty guidStringProperty = property.FindPropertyRelative("guidString");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(guidFieldRect, guidStringProperty, GUIContent.none);
            EditorGUI.EndDisabledGroup();

            if (GUI.Button(generateButtonRect, "new"))
            {
                guidStringProperty.stringValue = Guid.NewGuid().ToString();
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

}