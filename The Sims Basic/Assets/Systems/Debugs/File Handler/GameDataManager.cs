using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    string saveFile;

    [SerializeField]
    public ObjectData gameData = new ObjectData("Chair");

    [SerializeField]
    bool saveGame = false;
    [SerializeField]
    bool readGame = false;

    void Awake()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";
        



        //Log location of files
        //Debug.Log(saveFile);
    }

    void Update()
    {
        if(saveGame == true)
        {
            saveGame = false;
            writeFile();
        }

        if (readGame == true)
        {
            readGame = false;
            readFile();
        }
    }

    public void readFile()
    {
        //Check if file exist
        if (File.Exists(saveFile))
        {
            Debug.Log("game files exist");

            //Read file
            string fileContents = File.ReadAllText(saveFile);

            //Return int a pattern matching the game data class
            gameData = JsonUtility.FromJson<ObjectData>(fileContents);
        }
        else
        {
            Debug.Log("File does no exist");
            File.Create(Application.persistentDataPath + "/gamedata.json");
            readFile();
        }
    }


    public void writeFile()
    {
        if (File.Exists(saveFile))
        {
            //write file in json format
            string jsonString = JsonUtility.ToJson(gameData);

            File.WriteAllText(saveFile, jsonString);
        }
        else
        {
            Debug.Log("File does no exist");
            File.Create(Application.persistentDataPath + "/gamedata.json");
            writeFile();
        }
       
    }

    /// <summary>
    /// Check if a file is valid
    /// </summary>
    /// <returns></returns>
    bool CheckFileValidity(string url)
    {
        if (File.Exists(url))
        {
            Debug.Log("Game object data exist");
            return true;
        }
        else
        {
            Debug.Log("Url " + url + " does not exist");
            return false;    
        }
    }





}
