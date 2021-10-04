using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//Class that saves onto a file the objects and actions associated to it. 
public class ObjectSaver 
{

    // The save file containing objects
    string saveFileLocation;


    /// <summary>
    /// Object saver that takes name of the file you want to save as
    /// </summary>
    /// <param name="objectFileName"></param>
    public ObjectSaver(string objectFileName = "/objectData.json")
    {
        saveFileLocation = Application.persistentDataPath + objectFileName;
        if (File.Exists(saveFileLocation))
        {
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
        string stringData = JsonUtility.ToJson(objectdata);

        File.WriteAllText(saveFileLocation, stringData);
    }

    public List<ObjectData> LoadCurrentFileData()
    {
        //convert the file content to a string
        string fileContents = File.ReadAllText(saveFileLocation);
        //convert string to a list of object data
        return JsonUtility.FromJson<List<ObjectData>>(fileContents);
    }

    

    
    









}
