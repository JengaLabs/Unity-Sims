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

        gameEventManager = GameManager.Instance.GetGameEventManager();


        //Subscribe to method to resume game 
        gameEventManager.onTogglePause += UnPause;

        //Subscribe method to game resume delegate 
        gameEventManager.onEscapeButton += TogglePause;


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
    }



    /// <summary>
    /// Resume normal camera mode
    /// </summary>
    private void ResumeGame()
    {
        nextState = new NormalCam(camera);
        stage = EVENT.EXIT;
    }


    /// <summary>
    /// For when the camera wants to unpause game
    /// </summary>
    private void TogglePause()
    {
        gameEventManager.onEscapeButton -= UnPause;
        gameEventManager.onTogglePause -= TogglePause;

        //Call all pause game events
        gameEventManager.CallTogglePause();

        ResumeGame();
    }


    /// <summary>
    /// When another object wants to pause game
    /// </summary>
    private void UnPause()
    {
        gameEventManager.onEscapeButton -= TogglePause;
        gameEventManager.onTogglePause -= UnPause;

        ResumeGame();
    }


}
