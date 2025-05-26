using System.Text;
using TMPro;
using UnityEngine;

namespace karin
{
    public class TextSetter : MonoBehaviour
    {
        TMP_Text _targetText;
        [SerializeField, TextArea(1, 3)] private string _start;
        [SerializeField, TextArea(1, 3)] private string _end;

        private void Awake()
        {
            _targetText = GetComponent<TMP_Text>();
        }

        public void SetText(string middleText, bool onlyMiddleText = false)
        {
            StringBuilder builder = new StringBuilder();
            if (onlyMiddleText)
            {
                builder.Append(middleText);
            }
            else
            {
                builder.Append(_start);
                builder.Append(middleText);
                builder.Append(_end);
            }
            _targetText.text = builder.ToString();
        }


    }
}
