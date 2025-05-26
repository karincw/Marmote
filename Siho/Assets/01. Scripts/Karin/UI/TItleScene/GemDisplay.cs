using karin.Core;
using UnityEngine;

namespace karin
{
    public class GemDisplay : MonoBehaviour
    {
        private TextSetter _textSetter;

        private void Awake()
        {
            _textSetter = GetComponentInChildren<TextSetter>();
            DataLinkManager.Instance.Gem.OnValueChanged += HandleGemChanged;
        }

        private void OnDestroy()
        {
            DataLinkManager.Instance.Gem.OnValueChanged -= HandleGemChanged;
        }

        private void HandleGemChanged()
        {
            _textSetter.SetText(DataLinkManager.Instance.Gem.Value.ToString());
        }
    }
}