using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    public string _ActionName = "";


    public Action(string ActionName = "")
    {
        _ActionName = ActionName;

    }

    public void CallGActions()
    {
        Perform();


    }

    public abstract void Perform();
}
