using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class SaveLoadInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptions;

        private CanvasGroup _descriptionLayout;
        private CanvasGroup _curtain;
        private CanvasGroup _opendGroup;

        private Button _saveButton;
        private Button _loadButton;
        private Button _deleteButton;

        private TMP_Text _curtainText;
        private int _openIndex;

        private void Awake()
        {
            _descriptionLayout = transform.Find("Layout").Find("Description").Find("Text").GetComponent<CanvasGroup>();
            Transform buttonLayout = transform.Find("Layout").Find("Buttons").Find("Layout");
            _curtain = transform.Find("Layout").Find("Description").Find("Curtain").GetComponent<CanvasGroup>();
            _curtainText = _curtain.GetComponentInChildren<TMP_Text>();
            _saveButton = buttonLayout.Find("SaveBtn").GetComponent<Button>();
            _loadButton = buttonLayout.Find("LoadBtn").GetComponent<Button>();
            _deleteButton = buttonLayout.Find("DeleteBtn").GetComponent<Button>();
            SetUp();
        }

        public void ViewDescription(int runIndex)
        {
            _openIndex = runIndex;
            RunData? runData = Load.Instance.GetRunData(runIndex);
            if (!runData.HasValue || !runData.Value.load)
            {
                _curtainText.text = "슬롯에 데이터가 없어요";
                OpenGroup(_curtain);
                return;
            }
            RunData data = runData.Value;
            _curtainText.text = "슬롯을 선택해 주세요";
            OpenGroup(_descriptionLayout);
            StringBuilder sb = new StringBuilder();
            sb.Append($"Slot{data.runIndex}\n");
            sb.Append($"Stage:{data.stageIndex}\nTheme:{data.stageTheme}\n");
            sb.Append($"Coin:{data.coin}\n");
            sb.Append($"Characters:{data.characterType[0].type}");
            for (int i = 1; i < data.characterType.Length; i++)
            {
                sb.Append($", {data.characterType[i].type}");
            }
            _descriptions.text = sb.ToString();
        }

        private void OpenGroup(CanvasGroup open)
        {
            Utils.FadeCanvasGroup(_opendGroup, true, 0);
            Utils.FadeCanvasGroup(open, false, 0);
            _opendGroup = open;
        }

        public void SetUp()
        {
            _descriptions.text = "";
            OpenGroup(_curtain);
        }

        public void NewPlayMode()
        {
            _saveButton.onClick.RemoveAllListeners();
            _loadButton.onClick.RemoveAllListeners();
            _deleteButton.onClick.RemoveAllListeners();

            _saveButton.gameObject.SetActive(true);
            _saveButton.onClick.AddListener(() =>
            {
                Save.Instance.slotIndex = _openIndex;
                StartingPanel sp = FindFirstObjectByType<StartingPanel>();
                Load.Instance.gameData.cardLockData = sp.GetCardLockData();
                Load.Instance.saveRunDatas[_openIndex] = default;
                SceneChanger.Instance.LoadScene("WorldMap");
            });
            _loadButton.gameObject.SetActive(false);
            _deleteButton.gameObject.SetActive(true);
            _deleteButton.onClick.AddListener(() =>
            {
                Save.Instance.RemoveFile(_openIndex);
                ViewDescription(_openIndex);
            });
        }
        public void LoadPlayMode()
        {
            _saveButton.onClick.RemoveAllListeners();
            _loadButton.onClick.RemoveAllListeners();
            _deleteButton.onClick.RemoveAllListeners();

            _saveButton.gameObject.SetActive(false);
            _loadButton.gameObject.SetActive(true);
            _loadButton.onClick.AddListener(() =>
            {
                Save.Instance.slotIndex = _openIndex;
                Load.Instance.LoadAndApplyGame(_openIndex);
                StartingPanel sp = FindFirstObjectByType<StartingPanel>();
                Load.Instance.gameData.cardLockData = sp.GetCardLockData();
                SceneChanger.Instance.LoadScene("WorldMap");
            });
            _deleteButton.gameObject.SetActive(true);
            _deleteButton.onClick.AddListener(() =>
            {
                Save.Instance.RemoveFile(_openIndex);
                ViewDescription(_openIndex);
            });
        }
    }
}