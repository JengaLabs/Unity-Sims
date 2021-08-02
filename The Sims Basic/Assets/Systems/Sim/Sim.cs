using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim : GAgent
{

    private InputClass inputClass;

    private int GameWorldSpeed; 
    
    
    private void Awake()
    {
        GameManager.Instance.SelectSim(this);
       
        
        inputClass = GameManager.Instance.GetInputClass();
        inputClass.onLeftClickedEnviroment += AddLocationGoal;
        
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

        //Invoke("GetCommand", Random.Range(10, 20));
        
    }
    
    void GetCommand()
    {
        beliefs.ModifyState("CommandGiven", 0);
        Invoke("GetCommand", Random.Range(10, 20));
    }




    public void CancelAllActions()
    {

    }

    public void AddLocationGoal(Vector3 i)
    {
        //Add goal 
        beliefs.ModifyState("CommandGiven", 1);

        GameObject tempGoal = new GameObject();
        tempGoal.transform.position = i;
        tempGoal.tag = "Goal Location";
        inventory.AddItem(tempGoal);
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
