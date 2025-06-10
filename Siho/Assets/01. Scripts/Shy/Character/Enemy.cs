using Shy.Info;
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

        public override void Init(Team _team, CharacterSO _data)
        {
            base.Init(_team, _data);
            pressCompo.Init(InfoType.Enemy, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Enemy, new Info.CharacterInfo(this, _data));
            });
        }
    }
}
