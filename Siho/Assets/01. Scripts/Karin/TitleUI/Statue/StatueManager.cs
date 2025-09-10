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
    public CharacterDataSO chData;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _stateText;
    private MainStat _originStat;
    private string _originText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        chData = Instantiate(chData);
        _originText = _stateText.text;
        _originStat = chData.mainStat;
    }

    private void Update()
    {
        _text.text = count.ToString();
        StatUpdate();
    }

    public void StatUpdate()
    {
        MainStat stat = default;
        foreach (var statue in _statues)
        {
            stat += statue.resultStat();
        }
        chData.mainStat = stat + _originStat;

        _stateText.text = _originText.Replace("A", chData.mainStat.STR.ToString());
        _stateText.text = _stateText.text.Replace("B", chData.mainStat.DEX.ToString());
        _stateText.text = _stateText.text.Replace("C", chData.mainStat.HP.ToString());
    }

}
