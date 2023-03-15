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

    //Camera anchor
    private GameObject myAnchor;

    //Store the camera object
    GameObject cameraObject;

    //Main Camera
    Camera camera;

    /// <summary>
    /// Object camera is mounted and rotates around.
    /// </summary>
    Vector3 anchorPoint
    {
        get { return myAnchor.transform.position; }
    }

    //Camera's height from anchor
    float camHeight;

    //Starting Mouse position
    Vector2 mouseOrigin;
    
    //Store how zoomed in user is
    float _CurrentZoomLevel = 10f;

    //Zoom limits
    float farZoomLimit = 22f;
    float nearZoomLimit = 1f;

    //If mouse is being used to rotate camera
    bool orbitCamera = false;
    




    //Constructor for normal cam
    public NormalCam(GameObject _camera)
        : base(_camera)
    {
        cameraObject = _camera;
        myAnchor = cameraObject.transform.parent.gameObject;
        camera = Camera.main;
    }

    public override void Enter()
    {
        //Obtain input class
        _InputClass = GameManager.Instance.GetInputClass();

        //Get camera height
        camHeight = cameraObject.transform.position.y;

        //Move to anchors front default position 
        MoveToAnchor();

        //Move camera to correct position for a frame
        CameraLookAtAnchor(cameraObject, anchorPoint);

       

        //Subcribe to input events needed
        //Held right click 
        _InputClass.onNothingRightClicked += SwitchCameraOrbitBool;

        _InputClass.onEscapeButton += PauseGame;


        //Move to update loop
        base.Enter();
    }

    public override void Update()
    {
        
        BasicMovement();

        if (Input.GetKey(KeyCode.B))
        {
            nextState = new CamFollow(cameraObject, GameManager.Instance.GetSelectedSim().gameObject, true);
            stage = EVENT.EXIT;
            return;
        }

        //Check if camera should be orbiting
        if (orbitCamera)
        {
            OrbitAnchor();
        }


        LocalSmoothScroll();


        //Wasd controls and shift multiplier
        //BasicMovement();

        //OrbitAnchor();




        //Switch to free cam
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            nextState = new CamFree(cameraObject);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        _InputClass.onNothingRightClicked -= SwitchCameraOrbitBool;
        _InputClass.onEscapeButton -= PauseGame;
    }


    /// <summary>
    /// WASD Controls to move around with shift multiplier for the anchor point
    /// </summary>
    private void BasicMovement()
    {
        /* Remove double speed check and have a seperate event change the camera move speed.
        //Temp check for speed multiplier 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25;
        }
        else
        {
            moveSpeed = 10;
        }
        */
        //Set x position
        myAnchor.transform.Translate(Vector3.forward * Time.deltaTime * VerticalInput * CameraMoveSpeed);
        //Set z position
        myAnchor.transform.Translate(Vector3.right * Time.deltaTime * HorizontalInput * CameraMoveSpeed);
        //Find y position
        myAnchor.transform.Translate(Vector3.down * Time.deltaTime * FixAnchorHeight(myAnchor.transform.position));
    }

    /// <summary>
    /// Returns a float based on wether the anchor should move up or down. 
    /// </summary>
    /// <param name="currentPos"></param>
    /// <returns></returns>
    private float FixAnchorHeight(Vector3 currentPos)
    {
        //store hit data
        RaycastHit hit;
        //store a ray facing downwards
        Ray ray = new Ray(currentPos, Vector3.down * 100);
        //If ray hits somthing underneath it and within 10 units on the default layer 
        if (Physics.Raycast(ray, out hit, 100f))
        {

            if (hit.transform.gameObject.layer != 0)
            {
                return 0;
            }
            //Debug.Log(hit.transform.name);
            //check the distance from that object
            float distance = currentPos.y - hit.point.y;

            if (distance > 6f)
            {
                //distance too big
                return 5f;
            }
            else if (distance < 4f)
            {
                //distance to small
                return -5f;
            }
        }


        return 0;
    }

   

    #region Mouse Movements

    /// <summary>
    /// Get the mouse position on relative to the screen.
    /// </summary>
    void GetMousePosition()
    {
        mouseOrigin = Input.mousePosition;
    }

    /// <summary>
    /// Hold right click and rotate around anchor when mouse held down.
    /// </summary>
    private void OrbitAnchor()
    {

        if (Input.GetMouseButton(1))
        {

            //Get mouse movement
            float xRot = Input.GetAxis("Mouse X");
            //Rotate camera around anchor
            //cameraObject.transform.RotateAround(myAnchor.transform.position, Vector3.up, xRot * Time.deltaTime * rotationMultiplier);
            //Rotate the anchor 
            myAnchor.transform.Rotate(Vector3.up, xRot);

            //Continue to look at anchor
            CameraLookAtAnchor(cameraObject, anchorPoint);
        }

        //Check if player stops camera orbit
        if (Input.GetMouseButtonUp(1))
        {
            //Set orbital bool to reverse
            SwitchCameraOrbitBool();
        }
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
        CameraLookAtAnchor(cameraObject, anchorPoint);
    }





    private void LocalSmoothScroll()
    {
        //Get scroll wheel input either +1 or -1
        float scrollDistance = Input.mouseScrollDelta.y;
        //Take from current scroll distance level
        _CurrentZoomLevel -= scrollDistance;
        //Clamp the zoom value 
        _CurrentZoomLevel = Mathf.Clamp(_CurrentZoomLevel, nearZoomLimit, farZoomLimit);

        Vector3 newPos = new Vector3(cameraObject.transform.localPosition.x , Mathf.Pow(1.2f, _CurrentZoomLevel), -_CurrentZoomLevel * 1.2f);

        //Check user scrolled
        if (scrollDistance != 0)
        {
            //Scroll is inbound
            if (_CurrentZoomLevel < farZoomLimit && _CurrentZoomLevel > nearZoomLimit)
            {
                //Debug.Log("Current zoom level is " + _CurrentZoomLevel);
                //Move camera to new position
                cameraObject.transform.localPosition = newPos;
            }
        }

        //Reset view
        CameraLookAtAnchor(cameraObject, anchorPoint);
    }

    #endregion


    /// <summary>
    /// On load camera is zoomed out and eases into anchor
    /// </summary>
    private void MoveToAnchor()
    {
        //Move to default local position
        cameraObject.transform.localPosition = new Vector3(0, 5, -10);
        //Reset zoom level
        _CurrentZoomLevel = 10f;
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
