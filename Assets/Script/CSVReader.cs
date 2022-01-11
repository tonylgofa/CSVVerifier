using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string path)
    {
        var list = new List<Dictionary<string, object>>();
        //TextAsset data = new TextAsset();

        string text = System.IO.File.ReadAllText(path);
        

        /*
        string tempDataString = data.text;
        int totalStringLength = data.text.Length;        
        
        for (int i = 0; i < totalStringLength; i++)
        {
            if(data.text[i] == '\"')
            {
                isOpenBlanket = !isOpenBlanket;
            }
            if (isOpenBlanket)
            {
                if(data.text[i] == '\r' || data.text[i] == '\n')
                {
                    if (data.text[i + 1] == '\r' || data.text[i + 1] == '\n')
                    {
                        tempDataString = tempDataString.Substring(0, i) + '`' + tempDataString.Substring(i + 2);
                    }
                    else
                    {
                        tempDataString = tempDataString.Substring(0, i) + '`' + tempDataString.Substring(i + 1);
                    }
                }
            }
        }*/

        var lines = Regex.Split(text, LINE_SPLIT_RE);

        List<string> linesList = new List<string>();
        foreach (string singleLine in lines)
        {
            linesList.Add(singleLine);
        }

        bool isOpenBlanket = false;

        for (int i = 0; i < linesList.Count; i++)
        {
            if (CountCharInString(linesList[i]) % 2 == 1)
            {
                isOpenBlanket = true;
            }

            if (isOpenBlanket)
            {
                while (CountCharInString(linesList[i + 1]) % 2 != 1)
                {
                    linesList[i] = linesList[i] + "\n" + linesList[i + 1];
                    linesList.RemoveAt(i + 1);
                }
                linesList[i] = linesList[i] + "\n" + linesList[i + 1];
                linesList.RemoveAt(i + 1);
                isOpenBlanket = false;
            }
        }

        if (linesList.Count <= 1) return list;

        var header = Regex.Split(linesList[0], SPLIT_RE);
        for (var i = 1; i < linesList.Count; i++)
        {

            var values = Regex.Split(linesList[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                if (value.Length > 0)
                {
                    if (value[0] == '\"')
                    {
                        value = value.Substring(1, value.Length - 1);
                    }
                    if (value[value.Length - 1] == '\"')
                    {
                        value = value.Substring(0, value.Length - 1);
                    }
                    //value = value.Replace("\\", "");
                    value = value.Replace("\"\"", "\"");
                }
                //value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    public static int CountCharInString(string text)
    {
        return text.Split('\"').Length - 1;
    }
}