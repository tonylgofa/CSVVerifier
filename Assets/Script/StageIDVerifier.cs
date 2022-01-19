using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageIDVerifier : MonoBehaviour
{
    List<(string, int)> _stageArr = new List<(string, int)>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckStageID()
    {
        int totalError = 0;
        for (int i = 1; i < LoadData.instance.mgameStageList.Count; ++i)
        {
            (string, int) temp;
            temp.Item1 = LoadData.instance.mgameStageList[i]["StageList"].ToString();
            temp.Item2 = i;

            _stageArr.Add(temp);
        }

        foreach((string, int) temp in _stageArr)
        {
            bool isBreak = false;
            for (int i = 1; i < LoadData.instance.mgameQuestionList.Count; ++i)
            {
                if(LoadData.instance.mgameQuestionList[i]["QPOOLID"].ToString() == temp.Item1)
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
                ++totalError;
                int index = temp.Item2 + 2;
                Debug.LogError(index + " " + temp.Item1);
            }
        }
        _stageArr.Clear();
        UIManager.instance.DisplayDone(totalError);
    }
}
