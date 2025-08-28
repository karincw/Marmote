using Shy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;

public class DataLinkManager : MonoBehaviour
{
    public static DataLinkManager instance;
    public CharacterDataSO characterData;
    public List<CharacterDataSO> enemys;
    public Stage stage;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        Debug.Log(scene.name);
        if(scene.name == "Map")
        {
            PlayerManager.Instance.Init(characterData);
        }
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
