using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPaused : CameraState
{
    /*
        Camera does not move and player can only interact with UI 
    */

    //Store reference to camera
    private GameObject camera;

    //store reference to world states
    private WorldStates worldStates;

    //Store reference to input manager
    private InputClass _InputClass;



    //Constructor for paused state
    public CamPaused(GameObject _camera)
        : base(_camera)
    {
        camera = _camera;
    }
    

    public override void Enter()
    {
        //Get reference to world states
        worldStates = GameManager.Instance.GetWorld();

        //Set world state to paused
        worldStates.AddState("World Paused", 1);

        //Get reference to input class
        _InputClass = GameManager.Instance.GetInputClass();

        //Subscribe method to game resume delegate 
        _InputClass.onEscapeButton += ResumeGame;

        


        //Open settings 


        //Enter update loop
        base.Enter();
    }

    public override void Update()
    {
        


    }

    public override void Exit()
    {
        //Set world state to unpaused
        worldStates.RemoveState("World Paused");
        //Unsubscribe to delegate 
        _InputClass.onEscapeButton -= ResumeGame;
    }

    private void ResumeGame()
    {
        //Call all pause game methods
        _InputClass.CallTogglePause();


        nextState = new NormalCam(camera);
        stage = EVENT.EXIT;
    }



}
