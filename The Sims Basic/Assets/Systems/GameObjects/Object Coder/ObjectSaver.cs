using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


//Class that saves onto a file the objects and actions associated to it. 
public class ObjectSaver 
{

    // The save file containing objects
    string saveFileLocation;

    ObjectDataList dataList = new ObjectDataList();


    /// <summary>
    /// Object saver that takes name of the file you want to save as
    /// </summary>
    /// <param name="objectFileName"></param>
    public ObjectSaver(string objectFileName = "/objectData.json")
    {
        saveFileLocation = Application.persistentDataPath + objectFileName;
        if (File.Exists(saveFileLocation))
        {
            Debug.Log(saveFileLocation);
            //Open up the file 
            Debug.Log("File already exist");
        }
        else
        {
            //Create the new save file
            File.Create(saveFileLocation);
        }
    
    }

    public void SaveGameActions(List<ObjectData> objectdata)
    {
        Debug.Log("Saving game data");
        dataList.dataList = objectdata;
        string stringData = JsonUtility.ToJson(dataList);

        File.WriteAllText(saveFileLocation, stringData);
    }

    public List<ObjectData> LoadCurrentFileData()
    {
        string fileContents = "";

        //convert the file content to a string
        try
        {
            fileContents = File.ReadAllText(saveFileLocation);
        }
        catch
        {
            Debug.Log("error");
        }


        //Debug.Log(fileContents);

        //Check if empty
        if (fileContents == null || fileContents == "")  
        {
            return new List<ObjectData>();
        }


        //Store the json 
        dataList = JsonUtility.FromJson<ObjectDataList>(fileContents);

        

        //convert string to a list of object data
        return dataList.dataList;
    }
}


[System.Serializable]
public class ObjectDataList
{
    public List<ObjectData> dataList;
}