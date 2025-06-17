using karin.Core;
using Shy;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class LoadButton : CanvasFadeObject
    {
        [SerializeField] private int _loadIndex;

        private TMP_Text _titleText;
        private TMP_Text _minionText;
        private TMP_Text _stageText;
        private RunSaveData? _runData;

        protected override void Awake()
        {
            base.Awake();
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _stageText = transform.Find("StageText").GetComponent<TMP_Text>();
            _minionText = transform.Find("MinionText").GetComponent<TMP_Text>();
            _canvasGroup.alpha = 1;
        }

        private void Start()
        {
            Button b = GetComponent<Button>();
            b.onClick.AddListener(() => Save.Instance.SaveGameData());
        }

        public void PlayAgain()
        {
            DataLinkManager.Instance.SetLoadData(_runData.Value);
            DataManager.Instance.SetLoadData(_runData.Value);
            //data.isBattle = SceneManager.GetActiveScene().name != "WorldMap";
            SceneChanger.Instance.LoadScene("WorldMap");
        }

        public void SetUpViewData()
        {
            _runData = Load.Instance.LoadRunData(_loadIndex);
            if (!_runData.HasValue)
            {
                _minionText.text = "";
                _stageText.text = "";
                _titleText.text = "Empty Save";
                Fade(false);
                return;
            }
            Fade(true);

            _titleText.text = $"Save {_loadIndex}";
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

        protected override void Fade(bool inOut)
        {
            _canvasGroup.interactable = inOut;
            _canvasGroup.blocksRaycasts = inOut;
        }
    }
}