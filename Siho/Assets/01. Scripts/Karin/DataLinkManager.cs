using Shy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DataLinkManager : MonoBehaviour
{
    public static DataLinkManager instance;
    public CharacterDataSO characterData;
    public List<CharacterDataSO> enemys;
    public StageSO stage;
    public StageData stageData;

    private string GameSavePath;


    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        GameSavePath = @$"{Application.persistentDataPath}\GameData.txt";
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += HandleSceneLoaded;
        stageData = LoadStageData();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        Debug.Log(scene.name);
        if (scene.name == "Map")
        {
            PlayerManager.Instance.Init(characterData);
        }
    }

    public void SetCharacterData()
    {
        characterData = StatueManager.Instance.chData;
    }

    public CharacterDataSO GetEnemy()
    {
        var list = enemys.OrderBy(t => Random.value).ToList();
        return list[0];
    }

    public void SaveStageData()
    {
        FileInfo file = new FileInfo(GameSavePath);
        if (file.Exists)
            file.Delete();

        FileStream fs = new FileStream(GameSavePath, FileMode.CreateNew);
        Encoding encoding = Encoding.UTF8;
        fs.Write(encoding.GetBytes(JsonUtility.ToJson(stageData)));
        fs.Close();
    }

    public StageData LoadStageData()
    {
        StageData data = default;
        try
        {
            FileStream fs = new FileStream(GameSavePath, FileMode.Open);
            byte[] readBuffer = new byte[1024];
            fs.Read(readBuffer);
            fs.Close();
            Encoding encoding = Encoding.UTF8;
            data = JsonUtility.FromJson<StageData>(encoding.GetString(readBuffer));
        }
        catch (Exception ex)
        {
            Debug.LogError($"파일이 존재하지 않음{ex.Message}");
            stageData = default;
            stageData.stageEnable = new bool[3] { true, false, false };
            SaveStageData();
            return stageData;
        }
        return data;

    }

    public void OpenNextStage()
    {
        stageData.stageEnable[stage.stageIndex] = true;
    }
}

[System.Serializable]
public struct StageData
{
    public bool[] stageEnable;
}
