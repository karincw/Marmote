using TMPro;
using UnityEngine;

namespace Shy
{
    public class Enemy : Character
    {
        private TextMeshProUGUI actSign;

        public override void Awake()
        {
            base.Awake();
            actSign = transform.Find("Act Sign").GetComponent<TextMeshProUGUI>();
        }

        protected override void DeadAnime()
        {
            base.DeadAnime();
            actSign.gameObject.SetActive(false);
        }
    }
}
