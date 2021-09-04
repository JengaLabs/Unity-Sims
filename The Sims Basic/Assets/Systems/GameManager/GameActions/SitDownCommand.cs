using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDownCommand : Action
{
    Sim currentSim;

    public override void Perform()
    {
        //Get the current sim 
        currentSim = GameManager.Instance.GetSelectedSim();
        
    }

}
