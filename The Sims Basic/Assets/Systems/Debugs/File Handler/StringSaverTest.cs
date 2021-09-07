using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StringSaverTest : MonoBehaviour
{

    public bool SaveGame = false;

    public myClass myObject = new myClass(5, 2f, "Jake");

    public string json = "";

    void Update()
    {
        if(SaveGame == true)
        {
            SaveGame = SaveClass();
        }
    }

    private bool SaveClass()
    {
        json = JsonUtility.ToJson(myObject);

        
        return false;
    }
    

}


[System.Serializable]
public class myClass
{
    public myClass(int Level, float time, string name)
    {
        this.level = Level;
        this.time = time;
        this.playerName = name;
    }
    public int level;
    public float time;
    public string playerName;


}