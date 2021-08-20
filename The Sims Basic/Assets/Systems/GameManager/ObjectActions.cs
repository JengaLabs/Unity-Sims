using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActions 
{

    public ObjectActions(string name)
    {
        name = objectName;
    }
    public string objectName;

    public List<Action> actions = new List<Action>();


    /// <summary>
    /// Amount of action options a object has
    /// </summary>
    /// <returns>Number of actions</returns>
    public int AmountOfActions()
    {
        return actions.Count;
    }

    /// <summary>
    /// Add a already made action to the list of actions
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action)
    {
        actions.Add(action);
    }

    /// <summary>
    /// Tempary method to get actions in object actions 
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public List<Action> GetActions(short start = 0)
    {
        return actions.GetRange(start, actions.Count);
    }

    public Action GetAction(int index)
    {
        return actions[index];
    }
}
