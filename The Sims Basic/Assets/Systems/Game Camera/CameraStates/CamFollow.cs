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
    
    /// <summary>
    /// Reference to actual camera object.
    /// </summary>
    private GameObject Camera;

    /// <summary>
    /// Object camera is mounted and rotates around.
    /// </summary>
    private GameObject MyCameraAnchor;

    /// <summary>
    /// Object that should be followed
    /// </summary>
    private GameObject TrackingObject;

    /// <summary>
    /// If camera should continue following object
    /// </summary>
    private bool _ContinueFollowing = false;

    

    

    /// <summary>
    /// Camera will follow a given object
    /// </summary>
    /// <param name="_camera">Camera Object</param>
    /// <param name="continueFollowing">Enter true to continue following the given object.</param>
    /// <param name="ObjectToFollow">The Object you wish to follow. </param>
    public CamFollow(GameObject _camera, GameObject ObjectToFollow , bool continueFollowing = false)
        : base(_camera)
    {
        //get the camera object
        Camera = _camera;
        //Get the camera anchor
        MyCameraAnchor = Camera.transform.parent.gameObject;

        //Get the object the camera should be following
        TrackingObject = ObjectToFollow;
    }

    public override void Enter()
    {
        if(_ContinueFollowing == true)
        {
            MoveCameraTooPosition(TrackingObject.transform.position);
        }


        base.Enter();
    }

    public override void Update()
    {
        

        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Help");
            nextState = new NormalCam(Camera);
            stage = EVENT.EXIT;
        }

        
        MoveCameraTorwardsPosition(TrackingObject.transform.position);
        


    }

    public override void Exit()
    {
        Debug.Log("Leaving");
        base.Exit();
    }



    /// <summary>
    /// Move camera torwards object
    /// </summary>
    /// <param name="position"></param>
    private void MoveCameraTorwardsPosition(Vector3 position)
    {
        //Move the camera anchor to the correct position
        MyCameraAnchor.transform.position = Vector3.MoveTowards(MyCameraAnchor.transform.position, position, CameraMoveSpeed);

        //Look at anchor
        CameraLookAtAnchor(Camera, MyCameraAnchor.transform.position);
    }


    /// <summary>
    /// Continue moving camera torwards a given location.
    /// </summary>
    /// <param name="position"></param>
    private void MoveCameraTooPosition(Vector3 position)
    {
        //Move the camera anchor to the correct position
        MyCameraAnchor.transform.position = Vector3.MoveTowards(MyCameraAnchor.transform.position, position, CameraMoveSpeed);

        //Look at anchor
        CameraLookAtAnchor(Camera, MyCameraAnchor.transform.position);

        if(Vector3.Distance(MyCameraAnchor.transform.position, position) < 1f)
        {
            nextState = new NormalCam(Camera);
            stage = EVENT.EXIT;
        }
    }

}
