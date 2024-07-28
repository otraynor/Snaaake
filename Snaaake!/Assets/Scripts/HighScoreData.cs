using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreData : MonoBehaviour
{
    private string _dataPath;
    private string _textFile;

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
        _textFile = _dataPath + "Save_Data.txt";
    }
    
    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists");
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory already exists");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists");
            return;
        }
        
        File.WriteAllText(_textFile, "<SAVE DATA> \n");
        
        Debug.Log("New file created");
    }

    public void Initialize()
    {
        
        NewDirectory();
        NewTextFile();
        
    }
    
}