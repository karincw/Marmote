using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : MonoBehaviour
{
    public StageSO currentStageData;


    private TMP_Text _stageText;
    private Image _stageIcon;

    private CanvasGroup _mainCanvas;
    private CanvasGroup _stageChange;

    private void Awake()
    {
        _mainCanvas = transform.Find("ScreenLayout").GetComponent<CanvasGroup>();
        _stageChange = transform.Find("StageChangePanel").GetComponent<CanvasGroup>();
        _stageText = transform.Find("ScreenLayout").Find("StageLayout").Find("StageText").GetComponent<TMP_Text>();
        _stageIcon = transform.Find("ScreenLayout").Find("StageLayout").Find("MapIcon").GetComponent<Image>();
    }

    public void SetStage(StageSO stage)
    {
        currentStageData = stage;
        _stageText.text = stage.stageName;
        _stageIcon.sprite = stage.stageImage;
    }

    public void OpenMain()
    {
        Utils.FadeGroup(_mainCanvas, 0, true);
        Utils.FadeGroup(_stageChange, 0, false);
    }

    public void OpenStageChange()
    {
        Utils.FadeGroup(_mainCanvas, 0, false);
        Utils.FadeGroup(_stageChange, 0, true);
    }
}
