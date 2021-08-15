using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToo : GAction
{

    

    public override bool PrePerform()
    {

        if (!beliefs.HasState("Standing"))
        {
            return false;
        }


        target = inventory.FindItemWithTag("Goal Location");

        if(target == null)
        {
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {

        if(target != null)
        {
            inventory.RemoveItem(target);
            DestroyImmediate(target);
            
        }

        beliefs.ModifyState("CommandGiven", -1);



        return true;
    }

}
