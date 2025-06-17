using System.Text;
using TMPro;
using UnityEngine;

namespace karin
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] private int _saveIndex;

        private TMP_Text _titleText;
        private TMP_Text _minionText;
        private TMP_Text _stageText;
        private RunSaveData? _runData;

        protected void Awake()
        {
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _stageText = transform.Find("StageText").GetComponent<TMP_Text>();
            _minionText = transform.Find("MinionText").GetComponent<TMP_Text>();
        }

        public void SetUpViewData()
        {
            _runData = Load.Instance.LoadRunData(_saveIndex);
            if (!_runData.HasValue)
            {
                _minionText.text = "";
                _stageText.text = "";
                _titleText.text = "Empty Save";
                return;
            }

            _titleText.text = $"Save {_saveIndex}";
            _stageText.text = $"Stage : {_runData.Value.stageIndex + 1}";
            StringBuilder sb = new StringBuilder();
            sb.Append("Minions : ");
            for (int i = 0; i < _runData.Value.minionCount; i++)
            {
                if (i != 0) sb.Append(", ");
                sb.Append(_runData.Value.playerMinions[i].value[0].ToString());
            }
            _minionText.text = sb.ToString();
        }

        public void SaveData()
        {
            Save.Instance.SaveRunData(_saveIndex);
        }
    }
}