using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFree : CameraState
{

    //Mouse is frozen and hidden,
    //Player can move with wasd and look around with mouse, zoom with mouse wheel
    //Exit with escape

    //Variables for camera paused

    //Movement variables
    float HorizontalInput;
    float VerticalInput;
    

    //Camera objecct
    GameObject cameraObject;
    //Main camera
    Camera camera;

    //speed multiplier camera can move at
    short moveSpeed = 10;

    //Zoom limits
    Vector2 FOVlimits = new Vector2(50, 70);
    


    //Constructor for state
    public CamFree(GameObject _camera)
        : base(_camera)
    {
        cameraObject = _camera;
        camera = Camera.main;
    }

    public override void Enter()
    {
        //Event that camera is in free state


        //Lock mouse and hide it
        Cursor.lockState = CursorLockMode.Locked;

        //Move to update
        base.Enter();


    }

    public override void Update()
    {
        //Debug.Log("Updating");
        //Wasd controls for moving around 
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        

        


        //movement
        cameraObject.transform.Translate(new Vector3(HorizontalInput, 0, VerticalInput) * moveSpeed * Time.deltaTime);

        //Zooming in 
        camera.fieldOfView -= Input.mouseScrollDelta.y;
        //Check in range
        if(camera.fieldOfView > FOVlimits.y)
        {
            camera.fieldOfView = FOVlimits.y;
        }
        else if(camera.fieldOfView < FOVlimits.x)
        {
            camera.fieldOfView = FOVlimits.x;
        }


        //Rotation variables
        float yRot = cameraObject.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y");
        float xRot = cameraObject.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X");
        //Rotation being applied
        cameraObject.transform.rotation = Quaternion.Euler(yRot, xRot, 0);


        //Check for exit condition
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
        }



    }

    public override void Exit()
    {


    }


}
