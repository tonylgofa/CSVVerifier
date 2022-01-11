using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadData : MonoBehaviour
{
    public static LoadData instance;
    public static int currentLevel = 2;
    public static bool isLastLevel = false;
    public List<Dictionary<string, object>> mgameQuestionList = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> mgameStageList = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> strandList = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> collectionStageList = new List<Dictionary<string, object>>();
    public static event Action OnDataLoaded;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadQuestionAnswer();
    }

    public void LoadQuestionAnswer()
    {
        mgameQuestionList = CSVReader.Read(Application.persistentDataPath + "/MGameQuestionData/questionPool.csv");
        mgameStageList = CSVReader.Read(Application.persistentDataPath + "/MGameQuestionData/stage.csv");
        strandList = CSVReader.Read(Application.persistentDataPath + "/StrandData/strand.csv");
        //strandList = CSVReader.Read("MGameQuestionData/strand");
        collectionStageList = CSVReader.Read(Application.persistentDataPath + "/CollectionStageData/stage.csv");
        OnDataLoaded?.Invoke();
    }

}
