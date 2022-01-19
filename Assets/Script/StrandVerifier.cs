using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandVerifier : MonoBehaviour
{
    List<(string, int)> _mGameStageIDArr = new List<(string, int)>();
    List<(string, int)> _collectorStageIDArr = new List<(string, int)>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckStrand()
    {
        string[] tempStageIDArr = new string[0];
        int totalError = 0;

        for (int i = 0; i < LoadData.instance.strandList.Count; ++i)
        {
            tempStageIDArr = LoadData.instance.strandList[i]["quiz_stage_list"].ToString().Split('#');
            foreach (string stageID in tempStageIDArr)
            {
                if (stageID != "")
                {
                    _mGameStageIDArr.Add((stageID, i));
                }
            }
        }

        for (int i = 0; i < LoadData.instance.strandList.Count; ++i)
        {
            tempStageIDArr = LoadData.instance.strandList[i]["collector_stage_list"].ToString().Split('#');
            foreach (string stageID in tempStageIDArr)
            {
                if (stageID != "")
                {
                    _collectorStageIDArr.Add((stageID, i));
                }
            }
        }


        for (int i = 0; i < _mGameStageIDArr.Count; ++i)
        {
            bool isBreak = false;
            for (int j = 1; j < LoadData.instance.mgameStageList.Count; ++j)
            {
                if(LoadData.instance.mgameStageList[j]["StageID"].ToString() == _mGameStageIDArr[i].Item1)
                {
                    isBreak = true;
                    break;
                }
            }
            if (isBreak)
            {
                isBreak = false;
                continue;
            }
            else
            {
                int index = _mGameStageIDArr[i].Item2 + 1;
                Debug.LogError("quiz_stage_list id:" + index + " " + _mGameStageIDArr[i].Item1);
                ++totalError;
            }
        }

        for (int i = 0; i < _collectorStageIDArr.Count; ++i)
        {
            bool isBreak = false;
            for (int j = 0; j < LoadData.instance.collectionStageList.Count; ++j)
            {
                if (LoadData.instance.collectionStageList[j]["stage_id"].ToString() == _collectorStageIDArr[i].Item1)
                {
                    isBreak = true;
                    break;
                }
            }
            if (isBreak)
            {
                isBreak = false;
                continue;
            }
            else
            {
                int index = _collectorStageIDArr[i].Item2 + 1;
                Debug.LogError("collector_stage_list id:" + index + " " + _collectorStageIDArr[i].Item1);
                ++totalError;
            }
        }

        _mGameStageIDArr.Clear();
        _collectorStageIDArr.Clear();
        UIManager.instance.DisplayDone(totalError);
    }
}
