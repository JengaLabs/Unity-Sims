using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim : GAgent
{



    private int GameWorldSpeed; 
    
    
    private void Awake()
    {

    }

    private new void Start()
    {
        base.Start();

        //All Sims want to be pasued 
        SubGoal s1 = new SubGoal("CommandsFollowed", 1, false);
        goals.Add(s1, 1);
        //Add goals 
        SubGoal s2 = new SubGoal("rested", 2, false);
        goals.Add(s2, 2);

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

    /// <summary>
    /// Change the speed behavoirs of the sim
    /// </summary>
    /// <param name="speed"></param>
    public void SetSimWorldSpeed(int speed)
    {
        
    }

    


}
