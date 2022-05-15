using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Modifier))]
public class ModifierDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);
    EditorGUI.indentLevel = 1;

    SerializedProperty type = property.FindPropertyRelative("Type");
    SerializedProperty description = property.FindPropertyRelative("Description");

    EditorGUI.PropertyField(new Rect(-20 + position.x, position.y, 20 + position.width / 4, position.height), type, GUIContent.none);
    EditorGUI.PropertyField(new Rect(-10 + position.x + position.width * 1 / 4, position.y, 10 + position.width * 3 / 4, position.height), description, GUIContent.none);

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 20f;
}