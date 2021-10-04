using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataBase
{
    //A list that hold onto objects 
    public Dictionary<string, ObjectData> objectData = new Dictionary<string, ObjectData>();

    //Object saver and unloader 
    public ObjectSaver objectSaver = new ObjectSaver();

    public ObjectDataBase()
    {

    }

    public void AddObject(string objectID, List<string> actions)
    {
        if (FindObjectByName(objectID))
        {
            ObjectData data = new ObjectData(objectID);
            //loop through each possible action
            foreach(string act in actions)
            {
                //add each action to that object
                data.AddAction(act);
            }
            //Create that object
            objectData.Add(objectID, data);
        }
        else
        {
            Debug.Log("OBJECT ALREADY EXIST");
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
            objectData[objectName].AddAction(actionName);
        }
    }

    public bool FindObjectByName(string objectId)
    {
        if (objectData.ContainsKey(objectId))
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
            if (objectData[ObjectID].actions.Count > 0)
            {
                return objectData[ObjectID].actions;
            }
            else
            {
                Debug.Log("Object does not have any actions");
            }
        }


        
        return null;
        
    }


}
