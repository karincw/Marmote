using Shy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLinkManager : MonoBehaviour
{
    public static DataLinkManager instance;
    public CharacterDataSO characterData;
    public List<CharacterDataSO> enemys;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCharacterData()
    {
        characterData = StatueManager.Instance.GetCharacterData();
    }

    public CharacterDataSO GetEnemy()
    {
        var list = enemys.OrderBy(t => Random.value).ToList();
        return list[0];
    }
}
