using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    //Store how zoomed in user is
    float currentZoomLevel = 10f;

    //Zoom limits
    float farZoomLimit = 22f;
    float nearZoomLimit = 10f;

    //If mouse is being used to rotate camera
    bool orbitCamera = false;
    //If keys are being used for typing
    bool userTyping = false;

    


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

        //Get the camera's anchor point
        anchorPoint = GameManager.Instance.GetCameraAnchorSpawnPos();

        //Get camera height
        camHeight = cameraObject.transform.position.y;

        //Move camera to correct position for a frame
        LookAtAnchor();

        //Subcribe to input events needed
        _InputClass.onNothingClicked += SwitchCameraOrbitBool;

        _InputClass.onEscapeButton += PauseGame;


        //Move to update loop
        base.Enter();
    }

    public override void Update()
    {
        if (!userTyping)
        {
            BasicMovement();
        }


        //Check if camera should be orbiting
        if (orbitCamera)
        {
            OrbitAnchor();
        }

        //Check if zooming in and out wiht mouse enabled
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //ScrollInput();
            ExponetialCameraScroll();
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
        _InputClass.onNothingClicked -= SwitchCameraOrbitBool;
        _InputClass.onEscapeButton -= PauseGame;
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
        //cameraObject.transform.Translate(new Vector3(HorizontalInput, 0, VerticalInput) * moveSpeed * Time.deltaTime);
        //Locking camera height
        //cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, camHeight, cameraObject.transform.position.z);

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
        //Rotation multiplier
        float rotationMultiplier = 200f;


        if (Input.GetMouseButton(1))
        {
            
            //Get mouse movement
            float xRot = Input.GetAxis("Mouse X");
            //Rotate camera
            cameraObject.transform.RotateAround(anchorPoint, Vector3.up, xRot * Time.deltaTime * rotationMultiplier);
            //Continue to look at anchor
            LookAtAnchor();
        }

        //Check if player stops camera orbit
        if (Input.GetMouseButtonUp(1))
        {
            //Set orbital bool to reverse
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
        if (orbitCamera == true)
        {
            //Set where the mouse was at the start
            GetMousePosition();
            //Free mouse and hide it 
            Cursor.lockState = CursorLockMode.Confined;
            //for now just hiding it
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    #endregion

    #region Scroll Movements

    /// <summary>
    /// Depreciated, use as reference for future work
    /// </summary>
    private void ScrollInput()
    {
        //Get scroll wheel input
        float scrollDistance = Input.mouseScrollDelta.y;

        //Store orignal pos
        Vector3 tempPos = cameraObject.transform.position;

        int yMult = -10;
        int xMult = -30;

        //Store only x and z values of camera
        Vector3 horizontalPlane = new Vector3(tempPos.x, anchorPoint.y, tempPos.z);

        //Find direction between camera and anchor on xz plane
        Vector3 dir = horizontalPlane - anchorPoint;
        Vector3 Ndir = (dir).normalized;

        
        cameraObject.transform.position += new Vector3(dir.x * xMult, yMult, dir.z * xMult) * Time.deltaTime * scrollDistance;

        //.transform.Translate(new Vector3(dir.x * xMult, 0, dir.z * xMult) * Time.deltaTime * scrollDistance);
        //cameraObject.transform.Translate(new Vector3(0, scrollDistance * yMult * forwardDistance.magnitude, 0) * Time.deltaTime);

        
        //cameraObject.transform.Translate(new Vector3(0, scrollDistance * yMult, 0) * Time.deltaTime);
        //cameraObject.transform.Translate(moveDir * xMult * scrollDistance * Time.deltaTime);

        float dist = Vector3.Distance(anchorPoint, cameraObject.transform.position);
        //Check distance is viable
        if (dist > 50 || dist < 3)
        {
            Debug.Log("Distance not valid");
            cameraObject.transform.position = tempPos;
        }


        //Reset view at anchor
        LookAtAnchor();
    }

    private void SmoothScroll()
    {
        //Get scroll wheel input
        float scrollDistance = Input.mouseScrollDelta.y;

        //Store the normalized camera pos
        Vector3 normalizedCameraPosition = cameraObject.transform.position.normalized;
        

       
        //Zoom in 
        if(scrollDistance > 0 && currentZoomLevel > nearZoomLimit)
        {
            

            //Subtract from zoom level and use largest values
            currentZoomLevel = Mathf.Max(currentZoomLevel - 5f, nearZoomLimit);
            cameraObject.transform.position = normalizedCameraPosition * currentZoomLevel;
            
        }//Zoom out
        else if(scrollDistance < 0 && currentZoomLevel < farZoomLimit)
        {
            //Add to current zoom level and choose whatever is smaller
            currentZoomLevel = Mathf.Min(currentZoomLevel + 10f, farZoomLimit);
            
            cameraObject.transform.position = normalizedCameraPosition * currentZoomLevel;   
        }

        Debug.Log(currentZoomLevel);

        LookAtAnchor();
    }


    private void ExponetialCameraScroll()
    {
        //Get scroll wheel input either +1 or -1
        float scrollDistance = Input.mouseScrollDelta.y;
        currentZoomLevel -= scrollDistance;
        //Clamp the zoom value 
        currentZoomLevel = Mathf.Clamp(currentZoomLevel, nearZoomLimit, farZoomLimit);

        //Get just the x and z direction
        Vector3 dir = new Vector3(cameraObject.transform.position.x, 0, cameraObject.transform.position.z) - new Vector3(anchorPoint.x, 0, anchorPoint.z);
        //Debug.DrawRay(cameraObject.transform.position, dir.normalized * -10, Color.red);


        //Calculate new position of camera 
        Vector3 newPos = new Vector3(cameraObject.transform.position.x , Mathf.Pow(1.2f, currentZoomLevel), cameraObject.transform.position.z);

        if(scrollDistance != 0)
        {
            if(currentZoomLevel < farZoomLimit && currentZoomLevel > nearZoomLimit)
            {
                cameraObject.transform.position = newPos;
                cameraObject.transform.Translate(-dir.normalized * scrollDistance * 5, Space.World);
            }
            
        }
        


        //Reset view at anchor
        LookAtAnchor();
        
    }

    #endregion


    /// <summary>
    /// On load camera is zoomed out and eases into anchor
    /// </summary>
    private void MoveToAnchor()
    {

    }

    #region Pause Game

    public void PauseGame()
    {
        //Call all pause game methods
        _InputClass.CallTogglePause();

        nextState = new CamPaused(cameraObject);
        stage = EVENT.EXIT;
    }


    #endregion


}
