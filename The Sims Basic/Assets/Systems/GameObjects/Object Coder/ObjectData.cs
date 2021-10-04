using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectData
{

    //the id of the object
    [SerializeField]
    string objectId;

    //Names of actions it can perform
    public List<string> actions = new List<string>();

    //object reference to the mesh
    Mesh mesh;

    //object size from it's corner
    Vector3 objectSize;

    //where object interacts 
    List<Vector3> interactionPoints = new List<Vector3>();

    public ObjectData(string objectID, Vector3 objectSize = new Vector3())
    {
        this.objectId = objectID;
        this.objectSize = objectSize;

    }

    /// <summary>
    /// Add an action to an object id 
    /// </summary>
    /// <param name="actionName"></param>
    public void AddAction(string actionName)
    {
        //Add an action to the possible actions of an object
        this.actions.Add(actionName);
    }


}
