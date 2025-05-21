using UnityEditor;

namespace karin
{
    [CustomEditor(typeof(SelectBtn))]
    public class SelectBtnEditor : Editor
    {
        SerializedProperty normalColor  ;
        SerializedProperty hoverColor   ;
        SerializedProperty disableColor ;
        SerializedProperty sellPrice    ;

        void OnEnable()
        {
            normalColor   = serializedObject.FindProperty("_normalColor");
            hoverColor    = serializedObject.FindProperty("_hoverColor");
            disableColor  = serializedObject.FindProperty("_selectedColor");
            sellPrice     = serializedObject.FindProperty("_sellPrice");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(normalColor  );
            EditorGUILayout.PropertyField(hoverColor   );
            EditorGUILayout.PropertyField(disableColor );
            EditorGUILayout.PropertyField(sellPrice    );

            serializedObject.ApplyModifiedProperties();
        }
    }
}
