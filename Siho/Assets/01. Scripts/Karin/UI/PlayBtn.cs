using karin;
using Shy;
using UnityEngine;
using UnityEngine.UI;

public class PlayBtn : MonoBehaviour
{
    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => Save.Instance.SaveCharacterLockData());
        btn.onClick.AddListener(() => DataManager.Instance.MakeDice());
        btn.onClick.AddListener(() => Save.Instance.SaveGameData());
    }
}
