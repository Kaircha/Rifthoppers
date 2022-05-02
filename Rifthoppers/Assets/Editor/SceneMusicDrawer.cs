using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneMusic))]
public class SceneMusicDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);
    EditorGUI.indentLevel = 1;

    SerializedProperty scene = property.FindPropertyRelative("scene");
    SerializedProperty start = property.FindPropertyRelative("start");
    SerializedProperty loop = property.FindPropertyRelative("loop");

    EditorGUI.PropertyField(new Rect(-20 + position.x + position.width * 0 / 3, position.y, 20 + position.width / 3, position.height), scene, GUIContent.none);
    EditorGUI.LabelField(new Rect(-10 + position.x + position.width * 1 / 3, position.y, 30, position.height), "=>");
    EditorGUI.PropertyField(new Rect(10 + position.x + position.width * 1 / 3, position.y, position.width / 3, position.height), start, GUIContent.none);
    EditorGUI.PropertyField(new Rect(position.x + position.width * 2 / 3, position.y, position.width / 3, position.height), loop, GUIContent.none);
        
    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 20f;
}
