using UnityEngine;

namespace Shy.Event
{
    public class BlackJackButton : Button
    {
        private GameObject lockImg;

        protected override void Awake()
        {
            base.Awake();
            lockImg = transform.Find("Lock").gameObject;
        }

        public void LockChange(bool _useable)
        {
            lockImg.SetActive(!_useable);
            useable = _useable;
        }
    }
}
