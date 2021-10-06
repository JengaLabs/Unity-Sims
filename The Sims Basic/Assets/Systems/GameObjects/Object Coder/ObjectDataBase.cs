using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataBase
{
    //A list that hold onto objects 
    public Dictionary<string, ObjectData> _objectData = new Dictionary<string, ObjectData>();

    //Object saver and unloader 
    private ObjectSaver objectSaver = new ObjectSaver();

    public ObjectDataBase()
    {
        //Storage for files 
        List<ObjectData> tempObjects = new List<ObjectData>();

        //Get the save file
        //tempObjects = objectSaver.LoadCurrentFileData();

        ObjectData tempData = new ObjectData("Chair");
        tempData.AddAction("Sit Down");

        tempObjects.Add(tempData);

        //Add those objects to the dictionary
        foreach(ObjectData obj in tempObjects)
        {
            //Add that object to the list
            AddObject(obj.GetName(), obj.actions);
        }
    }

    

    public void AddObject(string objectID, List<string> actions)
    {
        //Check if object already exist
        if (!FindObjectByName(objectID))
        {
            //Add that object to the list
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
            Debug.Log("Object Data Base does not contain " + objectId);
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
        Debug.Log("Object does not exist in database");

        List<string> tempString = new List<string>();
        tempString.Add("Enviroment");

        return tempString;
    }



}
