﻿using System.Collections;
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

        //Sims start by just standing 
        beliefs.AddState("Standing", 1);
    }

    private new void Start()
    {
        base.Start();

        

        //All Sims want to finish all goals
        SubGoal s1 = new SubGoal("CommandsFollowed", 1, false);
        goals.Add(s1, 1);
        
        

        

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
