using Shy;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using ShyDataManager = Shy.DataManager;

public class CharacterPanel : MonoBehaviour
{
    private Image _characterIcon;
    private TMP_Text _health;
    private TMP_Text _melee;
    private TMP_Text _defence;
    private CharacterSO _data;
    [SerializeField] private int _index;
    private Button _descriptionPanelBtn;
    public event Action<CharacterSO> OnDescriptionOpen;

    private void Awake()
    {
        _characterIcon = transform.Find("CharacterIcon").GetComponent<Image>();
        _health = transform.Find("Health").GetComponentInChildren<TMP_Text>();
        _melee = transform.Find("Melee").GetComponentInChildren<TMP_Text>();
        _defence = transform.Find("Defence").GetComponentInChildren<TMP_Text>();
        _descriptionPanelBtn = transform.Find("DescriptionOpenBtn").GetComponent<Button>();
        _descriptionPanelBtn.onClick.AddListener(OpenDescription);
    }

    public void SetData()
    {
        _data = ShyDataManager.Instance.minions[_index];
        Debug.Log(_data);
        if (_data == null)
        {
            //회색처리
            return;
        }

        _characterIcon.sprite = _data.sprite;
        _health.text = _data.stats.hp.ToString();
        _melee.text = _data.stats.str.ToString();
        _defence.text = _data.stats.def.ToString();
    }

    private void OpenDescription()
    {
        OnDescriptionOpen?.Invoke(_data);
    }

}
