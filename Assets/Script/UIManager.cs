using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text URLText;
    [SerializeField] Text versionText;
    public Text LoadingText;

    public static UIManager instance;


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

    // Start is called before the first frame update
    void Start()
    {
        URLText.text = Application.persistentDataPath;
        versionText.text = "v" + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDone(int totalError = 0)
    {
        LoadingText.text = "Done. " + totalError.ToString() + " errors found.";
    }
}
