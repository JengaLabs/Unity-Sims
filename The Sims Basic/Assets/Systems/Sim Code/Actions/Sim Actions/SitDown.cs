using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDown : GAction
{

    
   

    public override bool PrePerform()
    {
        //Check if chair already exist in item flow
        if (target != null)
        {
            
            return true;
        }
        else
        {
            //Check world for any avalbiles chairs 
            target = inventory.FindItemWithTag("Chair");

            if (target == null)
            {
                return false;
            }
        }

        




        return true;
    }

    public override bool PostPerform()
    {

        //Check if agent is close enough to use chair
        if (agent.remainingDistance <= 1.5f)
        {
            Debug.Log("too far from chair"); 
            return false;
        } 
        //Remove the standing state
        beliefs.ModifyState("Standing", -1);

        //Add the sitting state
        beliefs.AddState("Sitting", 1);

        return true;
    }

}
