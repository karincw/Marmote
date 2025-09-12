using UnityEngine;

public class DebugMode : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StatueManager.Instance.count += 1;
        }
    }
}
