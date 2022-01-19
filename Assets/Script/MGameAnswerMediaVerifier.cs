using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameAnswerMediaVerifier : MonoBehaviour
{
    List<(string, int)> _ansArr = new List<(string, int)>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadImage()
    {
        StartCoroutine(WaitLoadImage());
    }

    IEnumerator WaitLoadImage()
    {
        UIManager.instance.LoadingText.text = "Loading....";
        yield return new WaitForSeconds(0.01f);
        for (int i = 1; i < LoadData.instance.mgameQuestionList.Count; ++i)
        {
            for (int j = 1; j <= 4; j++)
            {
                (string, int) temp;
                temp.Item1 = LoadData.instance.mgameQuestionList[i]["a" + j + "m"].ToString().TrimStart('{').TrimEnd('}');
                temp.Item2 = i;
                if (temp.Item1 != "")
                {
                    _ansArr.Add(temp);
                }
            }
        }
        int totalError = 0;
        for (int i = 0; i < _ansArr.Count; ++i)
        {
            if (Loader.instance.LoadFile(Application.persistentDataPath + "/MGameQuestionData/AnswerMedia/" + _ansArr[i].Item1 + ".png") == false)
            {
                Debug.LogError(_ansArr[i].Item2 + 2 + " " + _ansArr[i].Item1);
                ++totalError;
            }
        }
        _ansArr.Clear();
        UIManager.instance.DisplayDone(totalError);
    }
}
