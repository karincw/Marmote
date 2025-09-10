using Shy;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    public static StatueManager Instance;

    public int count = 3;

    [SerializeField] private List<Statue> _statues;
    [SerializeField] private CharacterDataSO _chData;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _stateText;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        _text.text = count.ToString();
        _stateText.text = _stateText.text.Replace("A", _chData.mainStat.STR.ToString());
        _stateText.text = _stateText.text.Replace("B", _chData.mainStat.DEX.ToString());
        _stateText.text = _stateText.text.Replace("C", _chData.mainStat.STR.ToString());
        _stateText.text = _stateText.text.Replace("D", _chData.mainStat.INT.ToString());
    }

    public CharacterDataSO GetCharacterData()
    {
        _chData = Instantiate(_chData);
        MainStat stat = default;
        foreach (var statue in _statues)
        {
            stat += statue.resultStat();
        }
        _chData.mainStat += stat;
        return _chData;
    }

}
