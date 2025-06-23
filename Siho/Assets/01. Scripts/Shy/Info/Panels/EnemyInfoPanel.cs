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

            dice.SetText(BattleManager.Instance.GetEnemyDiceCount(_cInfo.character).ToString());

            base.UpdatePanelData(_data);

            transform.position = _cInfo.character.transform.position + new Vector3(-4, 0);
        }
    }
}
