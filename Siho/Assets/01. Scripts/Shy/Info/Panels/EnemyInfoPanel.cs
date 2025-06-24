using TMPro;
using UnityEngine;

namespace Shy.Info
{
    public class EnemyInfoPanel : CharacterInfoPanel
    {
        [SerializeField] private TextMeshProUGUI dice;

        public override void UpdatePanelData(IInfoData _data)
        {
            if (_data is not CharacterInfo _cInfo) return;

            base.UpdatePanelData(_data);
            dice.SetText(BattleManager.Instance.GetEnemyDiceCount(_cInfo.character).ToString());

            transform.position = _cInfo.character.transform.position + new Vector3(-4, 0);
        }
    }
}
