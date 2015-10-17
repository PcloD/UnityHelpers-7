using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEditor;

public class NameChanger : ScriptableWizard
{
    public enum NamingMode
    {
        RenameToBaseName,AddNumbers,ReplaceNumbers,StripNumbers
    }
    public bool runRecursively = false;
    public NamingMode mode = NamingMode.AddNumbers;
    public string baseName = "Object";
    public string format = "0";
    String strHelp = "Select Game Objects";
    GameObject[] gos;
    private Dictionary<string, int> counters; 

    void OnWizardUpdate()
    {
        helpString = strHelp;
        isValid = true;//(theMaterial != null);
    }

    void OnWizardCreate()
    {
        gos = Selection.gameObjects;
        counters = new Dictionary<string, int>();
        foreach (GameObject go in gos)
        {
            RenameObject(go);
        }
    }

    int GetCounter(string n)
    {
        int res = 1;
        if (counters.ContainsKey(n))
            res = counters[n];
        else
            counters[n] = res;
        counters[n]++;
        return res;
    }

    string StripNumbers(string str)
    {
        Regex r = new Regex("^(.*?)\\d*$");
        Match m = r.Match(str);
        if (!m.Success)
            return str;
        return m.Groups[1].Value;
    }
    void RenameObject(GameObject go)
    {
        switch (mode)
        {
            case NamingMode.RenameToBaseName:
                go.name = baseName + GetCounter(baseName).ToString(format);
                break;
            case NamingMode.AddNumbers:
                go.name += GetCounter(go.name).ToString(format);
                break;
            case NamingMode.ReplaceNumbers:
                go.name = StripNumbers(go.name)+ GetCounter(go.name).ToString(format);
                break;
            case NamingMode.StripNumbers:
                go.name = StripNumbers(go.name);
                break;
        }
    

        if (runRecursively)
        {
            foreach (Transform t in go.transform)
            {
                
                RenameObject(t.gameObject);
            }
            
        }
    }

    [MenuItem("Custom/Rename Objects", false, 4)]
    private static void nameChanger()
    {
        ScriptableWizard.DisplayWizard("Rename Objects", typeof (NameChanger), "Rename");
    }
}
