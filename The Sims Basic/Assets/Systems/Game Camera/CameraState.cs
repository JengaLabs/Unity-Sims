﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State machine base class
public class CameraState
{

    //Enum holding all camera states
    public enum CAMERA_STATE
    {
        PAUSED, //Enter the menu 
        TAKEAWAY, //Standard dtycoon game camera
        FREECAM, //More of a dev camera to get close up and personal
    }

    //Progress through states or the current Act
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }



    //Holds current states name for debuging
    public CAMERA_STATE stateName;
    //Act of the state
    protected EVENT stage;
    //Store the next state 
    public CameraState nextState;
    //The actual camera
    protected GameObject playerCamera;

    /// <summary>
    /// Cameras current Move speed.
    /// </summary>
    public float CameraMoveSpeed
    {
        get { return 10;  }
    }


    /// <summary>
    /// Camera input movement on the right axis.
    /// </summary>
    public float HorizontalInput
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    /// <summary>
    /// Camera input movement on the forward axis.
    /// </summary>
    public float VerticalInput
    {
        get { return Input.GetAxis("Vertical"); }
    }


    public CameraState(GameObject _camera)
    {
        this.playerCamera = _camera;

        stage = EVENT.ENTER;

    }

    //First act to run in state
    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
        
    }

    //Repeating act in state
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    //End of act, goes to next state
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public CameraState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }


    /// <summary>
    /// Rotate the camera to look at the anchor.
    /// </summary>
    /// <param name="CameraObject">Actual Camera</param>
    /// <param name="AnchorPosition">The position of the anchor.</param>
    public void CameraLookAtAnchor(GameObject CameraObject, Vector3 AnchorPosition)
    {
        CameraObject.transform.LookAt(AnchorPosition);
    }


}
