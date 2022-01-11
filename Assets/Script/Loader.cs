using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public static Loader instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LoadFile(string path)
    {
        if (string.IsNullOrEmpty(path)) return false;
        if (System.IO.File.Exists(path))
        {
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);
        //    try
        //    {
        //        Texture2D texture = new Texture2D(1, 1);
        //        texture.LoadImage(bytes);
        //        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //    }
        //    catch(Exception e)
        //    {
        //        Debug.LogError("cannot load image");
        //        return false;
        //    }
            return true;
        }
        return false;
    }
}
