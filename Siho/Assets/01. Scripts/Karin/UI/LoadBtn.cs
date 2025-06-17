using karin;
using UnityEngine;
using UnityEngine.UI;

public class LoadBtn : MonoBehaviour
{
    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => Save.Instance.SaveCharacterLockData());
    }
}
