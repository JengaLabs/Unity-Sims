using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTooLocation : Action
{

    Sim currentSim; 

    public GoTooLocation()
    {
        SetName("Go Here");
    }
    
    public override void Perform()
    {
        currentSim = GameManager.Instance.GetSelectedSim();
        //Add the object to the current sims inventory
        currentSim.AddLocationGoal(GameManager.Instance.GetInputClass().StoredLocation);
        //Debug.Log(currentSim.gameObject);
    }




}
