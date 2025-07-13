using UnityEngine;

public class EditorModeCheck : MonoBehaviour
{
    public static bool isEditorMode = true;

    private void Awake()
    {
        isEditorMode = false;
        Debug.Log("에디터 실행");
    }

    private void OnApplicationQuit()
    {
        isEditorMode = true;
        Debug.Log("에디터 종료");
    }
}
