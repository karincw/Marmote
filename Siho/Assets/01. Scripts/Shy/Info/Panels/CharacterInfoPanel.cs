using Shy.Unit;
using TMPro;
using UnityEngine;

namespace Shy.Info
{
    public class CharacterInfoPanel : InfoPanel
    {
        [SerializeField] private TextMeshProUGUI hp, atk, def;
        [SerializeField] private TextMeshProUGUI[] skills = new TextMeshProUGUI[3];

        protected string GetStatString(Character _ch, StatEnum _stat) => _ch.GetNowStat(_stat) + $"({_ch.GetBaseStat(_stat)}+{_ch.GetBonusStat(_stat)})";

        public override void UpdatePanelData(IInfoData _data)
        {
            if (_data is not CharacterInfo _cInfo) return;

            nameTmp.SetText("");

            hp.SetText(_cInfo.character.GetNowStat(StatEnum.Hp) + "/" + GetStatString(_cInfo.character, StatEnum.MaxHp));
            atk.SetText(GetStatString(_cInfo.character, StatEnum.Str));
            def.SetText(GetStatString(_cInfo.character, StatEnum.Def));

            for (int i = 0; i < 3; i++)
            {
                skills[i].SetText("");
            }
        }
    }
}
