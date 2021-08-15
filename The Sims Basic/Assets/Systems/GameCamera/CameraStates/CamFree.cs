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
    float moveSpeed = 10;

    //Zoom limits
    Vector2 FOVlimits = new Vector2(40, 80);

    //Starting height
    float cameraHeightPos;

    //Delegates to track input
    private InputClass _InputClass;



    //Constructor for state
    public CamFree(GameObject _camera)
        : base(_camera)
    {
        cameraObject = _camera;
        camera = Camera.main;
        cameraHeightPos = _camera.transform.position.y;
    }

    public override void Enter()
    {
        //Get input class 
        _InputClass = GameManager.Instance.GetInputClass();

        //Subscribe to escape event
        _InputClass.onEscapeButton += ExitFreeCam;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25f;
        }
        else
        {
            moveSpeed = 10f;
        }



        //movement
        cameraObject.transform.Translate(new Vector3(HorizontalInput, 0, VerticalInput) * moveSpeed * Time.deltaTime);
        //Locking camera's height
        cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraHeightPos, cameraObject.transform.position.z);
        
        


        //Zooming in 
        camera.fieldOfView -= Input.mouseScrollDelta.y;
        //Check in range of FOV limits
        if(camera.fieldOfView > FOVlimits.y)
        {
            camera.fieldOfView = FOVlimits.y;
        }
        else if(camera.fieldOfView < FOVlimits.x)
        {
            camera.fieldOfView = FOVlimits.x;
        }


        //Rotation variables
        float yRot = cameraObject.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y");
        float xRot = cameraObject.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X");
        //Rotation being applied
        cameraObject.transform.rotation = Quaternion.Euler(yRot, xRot, 0);


        



    }

    public override void Exit()
    {
        //Debug.Log("Escape");

        //Keep cursor in game
        Cursor.lockState = CursorLockMode.Confined;
        
        //Reset FOV 
        camera.fieldOfView = 60;

    }

    /// <summary>
    /// Transition to normal camera state
    /// </summary>
    private void ExitFreeCam()
    {
        //Unsubscribe from input manager delegates 
        _InputClass.onEscapeButton -= ExitFreeCam;

        //Set next state
        nextState = new NormalCam(cameraObject);
        //Go to next state 
        stage = EVENT.EXIT;

    }



}
