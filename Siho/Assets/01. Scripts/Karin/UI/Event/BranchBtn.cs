using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin.ui
{
    public class BranchBtn : MonoBehaviour
    {
        public Button button;
        public TMP_Text text;

        public void Awake()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<TMP_Text>();
        }
    }
}