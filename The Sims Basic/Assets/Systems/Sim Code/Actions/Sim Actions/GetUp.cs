using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUp : GAction
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

        Debug.Log("Getting Up");
        //Add chair back to useable objects
        beliefs.ModifyState("Standing", 1);

        beliefs.ModifyState("Sitting", -1);

        return true;
    }

}
