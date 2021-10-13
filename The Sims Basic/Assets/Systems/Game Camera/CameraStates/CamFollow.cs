using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : CameraState
{
    /*
        Camera Follow state involes the camera following a given object
    and allowing the player to rotate around it. 
    This will work by moving the anchor smoothly torwards the given objects position.
    The user will continue to rotate the camera as they would with a normal camera. 

    If the user enters the pause menu, the camera should switch to normal camera



    */
    //Store reference to camera
    private GameObject camera;

    //Reference to camera anchor
    private GameObject myAnchor;

    //Constructor for follow state
    public CamFollow(GameObject _camera)
        : base(_camera)
    {
        //get the camera object
        camera = _camera;
        //Get the camera anchor
        myAnchor = camera.transform.parent.gameObject;

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {


        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
