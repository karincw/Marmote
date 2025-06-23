using karin;
using TMPro;
using UnityEngine;

public class GemDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = $"Gem:{DataLinkManager.Instance.Gem}";
    }
}
