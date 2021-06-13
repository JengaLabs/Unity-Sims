using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCam : CameraState
{
    //Moves around with wasd and click/hold and drag
    //mouse is free and can click on gui/world

    //Delegates to track
    private InputClass _InputClass;



    //Store the camera object
    GameObject cameraObject;

    //Main Camera
    Camera camera;

    //Anchor Point for camera
    Vector3 anchorPoint;

    //Camera's height from anchor
    float camHeight;

    //Starting Mouse position
    Vector2 mouseOrigin;

    //Distance mouse moved
    float mouseMoveDistance = 0f;

    //If mouse is being tracked for movement
    bool orbitCamera = false;

    //If in a text box


    //Constructor for normal cam
    public NormalCam(GameObject _camera)
        : base(_camera)
    {
        cameraObject = _camera;
        camera = Camera.main;
    }

    public override void Enter()
    {
        //Obtain input class
        _InputClass = GameManager.Instance.GetInputClass();


        //Event that game is in normal mode

        //Confine mouse to screen 
        Cursor.lockState = CursorLockMode.Confined;

        //Get the camera's anchor point
        anchorPoint = GameManager.Instance.GetCameraAnchorSpawnPos();

        //For now set camera at anchor points height
        //cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, 0, cameraObject.transform.position.z);

        //Subcribe to input events needed
        _InputClass.onGuiInput += SwitchCameraOrbitBool;


        //Move to update loop
        base.Enter();
    }

    public override void Update()
    {
        //Check if camera should be orbiting
        if (orbitCamera)
        {
            OrbitAnchor();
        }

        



            //Wasd controls and shift multiplier
            //BasicMovement();

            //OrbitAnchor();

            //LookAtAnchor();



            //Switch to free cam
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                nextState = new CamFree(cameraObject);
                stage = EVENT.EXIT;
            }

    }

    public override void Exit()
    {

    }


    /// <summary>
    /// WASD Controls to move around with shift multiplier for the anchor point
    /// </summary>
    private void BasicMovement()
    {
        //Get WASD inputs
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        //For now a local var for speed multiplier
        float moveSpeed = 10;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25;
        }
        else
        {
            moveSpeed = 10;
        }

        //Movement
        cameraObject.transform.Translate(new Vector3(HorizontalInput, 0, VerticalInput) * moveSpeed * Time.deltaTime);
        //Locking camera height
        cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, camHeight, cameraObject.transform.position.z);

    }

    /// <summary>
    /// Switch keyboard movement to allow input from keyboard to effect anchor.
    /// </summary>
    private void SwitchToKeyboardMovement()
    {

    }

    

    #region Mouse Movements

    /// <summary>
    /// Get the mouse position on relative to the screen.
    /// </summary>
    void GetMousePosition()
    {
        mouseOrigin = Input.mousePosition;
        mouseMoveDistance = 0;
    }

    /// <summary>
    /// Hold right click and rotate around anchor when mouse held down.
    /// </summary>
    private void OrbitAnchor()
    {
        //Make sure you already trackced the starting pos of mouse

        //Percentage of screen the mouse moved across
        float movedPercent;
        //Rotation multiplier
        float rotationMultiplier = 5000f;


        if (Input.GetMouseButton(1))
        {
            //Set cursor texture
            //for now just hiding it
            Cursor.visible = true;

            //track distance moved from orignal pos
            mouseMoveDistance = Input.mousePosition.x - mouseOrigin.x;

            //Find percentage of screen mouse moved across
            movedPercent = mouseMoveDistance / Screen.width;

            //Rotate camera
            cameraObject.transform.RotateAround(anchorPoint, Vector3.up, movedPercent * Time.deltaTime * rotationMultiplier);

            //Make sure still looking at anchor
            LookAtAnchor();

            //reset mouse orgin
            mouseOrigin = Input.mousePosition;

        }


        //Check if player stops camera orbit
        if (Input.GetMouseButtonUp(1))
        {
            SwitchCameraOrbitBool();
        }
    }

    /// <summary>
    /// Rotate camera to look at anchor
    /// </summary>
    private void LookAtAnchor()
    {
        cameraObject.transform.LookAt(anchorPoint);
    }
    
    /// <summary>
    /// Switcch camera orbit to the opposite bool.
    /// </summary>
    private void SwitchCameraOrbitBool()
    {
        //Reverse what orbit camera equals
        orbitCamera = !orbitCamera;
        if(orbitCamera == true)
        {
            //Set where the mouse was at the start
            GetMousePosition();
        }
    }

    #endregion


    /// <summary>
    /// On load camera is zoomed out and eases into anchor
    /// </summary>
    private void MoveToAnchor()
    {

    }

}
