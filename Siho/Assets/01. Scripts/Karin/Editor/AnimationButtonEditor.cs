using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationButton))]
public class AnimationButtonEditor : Editor
{
    SerializedProperty baseColor;
    SerializedProperty baseImage;
    SerializedProperty pressedColor;
    SerializedProperty pressedImage;
    SerializedProperty disabledColor;

    void OnEnable()
    {
        baseColor = serializedObject.FindProperty("_baseColor");
        baseImage = serializedObject.FindProperty("_baseImage");
        pressedColor = serializedObject.FindProperty("_pressedColor");
        pressedImage = serializedObject.FindProperty("_pressedImage");
        disabledColor = serializedObject.FindProperty("_disabledColor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(baseColor);
        EditorGUILayout.PropertyField(baseImage);
        EditorGUILayout.PropertyField(pressedColor);
        EditorGUILayout.PropertyField(pressedImage);
        EditorGUILayout.PropertyField(disabledColor);

        serializedObject.ApplyModifiedProperties();
    }
}

