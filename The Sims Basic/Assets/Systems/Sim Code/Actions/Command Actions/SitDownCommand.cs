using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDownCommand : Action
{
    Sim currentSim;

    public SitDownCommand()
    {
        SetName("Sit Down");
    }
    
    public override void Perform()
    {
        //Get the current sim 
        currentSim = GameManager.Instance.GetSelectedSim();
        //add the selected chair to agent inventory 
        currentSim.inventory.AddItem(GameManager.Instance.GetInputClass().StoredObject);
        //Create goal to sit down
        //Add the goal to want to sit
        SubGoal s3 = new SubGoal("Sitting", 1, true);
        //add goal to sit down 
        currentSim.AddGoal(s3);

    }

}
