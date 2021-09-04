using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectActionsStorage
{
    //Store all objects names and assoicated actions groups
    public Dictionary<string, ObjectActions> ObjectActions = new Dictionary<string, ObjectActions>();


    public ObjectActions GetObjectActions(string ObjectName)
    {
        if (ObjectActions.ContainsKey(ObjectName))
        {
            return ObjectActions[ObjectName];
        }
        else
        {
            Debug.Log(ObjectName + " does not exist in the Object Actions Storage");
            return ObjectActions["Enviroment"];
        }
    }



    public ObjectActionsStorage()
    {

        EnviromentActions.AddAction(new GoTooLocation());
        ObjectActions.Add("Enviroment", EnviromentActions);

        ChairActions.AddAction(new SitDownCommand());
        ObjectActions.Add("Chair", ChairActions);
    }

    #region Enviroment Actions 

    private ObjectActions EnviromentActions = new ObjectActions("Enviroment");



    #endregion

    #region Interactable Actions

    private ObjectActions ChairActions = new ObjectActions("Chair");

    #endregion
}
