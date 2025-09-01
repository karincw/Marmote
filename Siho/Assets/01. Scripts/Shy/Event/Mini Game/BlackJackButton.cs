using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class BlackJackButton : MonoBehaviour
    {
        internal UnityAction onClickEvent;

        private bool useable;
        private GameObject lockImg;

        private void Awake()
        {
            lockImg = transform.Find("Lock").gameObject;
        }

        public void OnClick()
        {
            if (!useable) return;

            onClickEvent?.Invoke();
        }

        public void LockChange(bool _useable)
        {
            lockImg.SetActive(!_useable);
            useable = _useable;
        }
    }
}
