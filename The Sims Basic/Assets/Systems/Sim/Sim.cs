using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim : GAgent
{

    
    
    
    private void Awake()
    {

    }

    private new void Start()
    {
        base.Start();

        //All Sims want to be pasued 

        //Add goals 
        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 1);

        Invoke("GetTired", Random.Range(10, 20));

    }
    
    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke("GetTired", Random.Range(10, 20));
    }




    public void CancelAllActions()
    {

    }

    public void AddObject(GameObject i)
    {
        inventory.AddItem(i);
    }

    public void AddGoal(SubGoal subGoal)
    {
        goals.Add(subGoal, 1);
    }

    


}
