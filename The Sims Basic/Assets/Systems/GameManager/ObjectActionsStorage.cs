using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectActionsStorage
{
    //Actions names and their action class
    public Dictionary<string, Action> ActionValues = new Dictionary<string, Action>();

    public ObjectActionsStorage()
    {

        //TODO store this list in a save file so commands can be manipulated to what they corrospond to and what can be added 
        ActionValues.Add("Sit Down", new SitDownCommand());
        ActionValues.Add("Enviroment", new GoTooLocation());


       
    }

    public Action GetActionByName(string actionName)
    {
        //Check if that action exist
        if (ActionValues.ContainsKey(actionName))
        {
            //return that action 
            return ActionValues[actionName];
        }
        else
        {
            Debug.Log("Action " + actionName + " does not exist in storage.");

            //Return a standard action for now
            return new GoTooLocation();

        }
    }

}
