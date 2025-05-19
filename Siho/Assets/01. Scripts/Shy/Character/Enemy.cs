using TMPro;
using UnityEngine;

namespace Shy
{
    public class Enemy : Character
    {
        [SerializeField] private TextMeshProUGUI actSign;

        protected override void DeadAnime()
        {
            base.DeadAnime();
            actSign.gameObject.SetActive(false);
        }
    }
}
