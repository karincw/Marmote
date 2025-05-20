using System;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private List<IHaveSaveData> saveDatas = new List<IHaveSaveData>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [ContextMenu("SaveData")]
    public void SaveDataJson()
    {
        SaveData saveData = new SaveData();
        foreach (IHaveSaveData s in saveDatas)
        {
            s.GetSaveData(ref saveData);
        }

        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
    }

}

public interface IHaveSaveData
{
    public void GetSaveData(ref SaveData save);
}

[Serializable]
public struct SaveData
{
    public int theme;
    public int stageIndex;
    public int stagePosition;
    public List<int> tileData;
    public int isBattle;
    public int pfType;
    public List<int> pfStat;
    public int pfHealth;
    public int psType;
    public List<int> psStat;
    public int psHealth;
    public int ptType;
    public List<int> ptStat;
    public int ptHealth;
}