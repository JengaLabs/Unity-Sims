using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTooLocation : Action
{

    Sim currentSim; 
    public override void Perform()
    {
        currentSim = GameManager.Instance.GetSelectedSim();
        currentSim.AddLocationGoal(GameManager.Instance.GetInputClass().StoredLocation);
        Debug.Log(currentSim.gameObject);
    }




}
