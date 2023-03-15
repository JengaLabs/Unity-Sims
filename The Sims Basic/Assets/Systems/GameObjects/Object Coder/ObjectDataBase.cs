using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectDataBase
{
    //A list that hold onto objects 
    public Dictionary<string, ObjectData> _objectData = new Dictionary<string, ObjectData>();

    //Object saver and unloader 
    private ObjectSaver _objectSaver = new ObjectSaver();

    private InputClass _inputClass;

    public ObjectDataBase()
    {
        //Storage for files 
        List<ObjectData> tempObjects = new List<ObjectData>();

        //Input class to listen to 
        _inputClass = GameManager.Instance.GetInputClass();

        

        //Get the save file
        tempObjects = _objectSaver.LoadCurrentFileData();

        /*
        ObjectData tempObject = new ObjectData("Enviroment");
        tempObject.actions.Add("Go Too");
        tempObjects.Add(tempObject);
        */

        //Add those objects to the dictionary
        foreach(ObjectData obj in tempObjects)
        {
            //Debug.Log(obj.GetName());
            
            //Add that object to the list
            AddObject(obj.GetName(), obj.actions);
        }

        //Add calls 
        _inputClass.OnSave += SaveGame;
    }

    

    public void AddObject(string objectID, List<string> actions)
    {
        //Check if object already exist
        if (!FindObjectByName(objectID))
        {
            //Add that object to the list if it does not exist
            ObjectData data = new ObjectData(objectID);
            //loop through each possible action
            foreach(string act in actions)
            {
                //add each action to that object
                data.AddAction(act);
            }
            //Create that object
            _objectData.Add(objectID, data);
        }
        else
        {
            //Add object if it exist
            Debug.Log("OBJECT ALREADY EXIST");


            /*
            //Check if any new actions are attached
            foreach (string act in actions)
            {
                //If an action that doesnt exist yet is inside actions
                if (!_objectData[objectID].actions.Contains(act))
                {
                    //Add that action to the group
                    _objectData[objectID].AddAction(act);
                }
            }
            */

        }
    }


    /// <summary>
    /// Adds an action to an object
    /// </summary>
    /// <param name="Action"></param>
    public void AddAction(string objectName, string actionName)
    {
        //check if object already exist
        if (FindObjectByName(objectName))
        {
            //add action to that object
            _objectData[objectName].AddAction(actionName);
        }
    }

    //Check if object exist
    public bool FindObjectByName(string objectId)
    {
        if (_objectData.ContainsKey(objectId))
        {
            return true;
        }
        else
        {
            //Debug.Log("Object Data Base does not contain " + objectId);
            return false;
        }
    }

    public List<string> GetObjectActions(string ObjectID)
    {
        if (FindObjectByName(ObjectID))
        {
            if (_objectData[ObjectID].actions.Count > 0)
            {
                return _objectData[ObjectID].actions;
            }
            else
            {
                Debug.Log("Object does not have any actions");
            }
        }


        
        return null;
        
    }

    public List<string> GetActions(string objectID)
    {
        if (FindObjectByName(objectID))
        {
            return _objectData[objectID].actions;

        }

        //Log that the objet did not exist
        Debug.Log("Actions for object " + objectID + " is not in the database");

        List<string> tempString = new List<string>();
        tempString.Add("Enviroment");

        return tempString;
    }

    public void SaveGame()
    {
        //Convert type to list
        List<ObjectData> tempData = _objectData.Values.ToList();

        _objectSaver.SaveGameActions(tempData);
    }



}
