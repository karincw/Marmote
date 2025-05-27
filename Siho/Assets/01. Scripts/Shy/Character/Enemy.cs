using TMPro;
using UnityEngine;

namespace Shy.Unit
{
    public class Enemy : Character
    {
        [SerializeField] private TextMeshProUGUI actSign;
        internal int actionValue = 1;

        protected override void DeadAnime()
        {
            base.DeadAnime();
            actSign.gameObject.SetActive(false);
        }
    }
}
