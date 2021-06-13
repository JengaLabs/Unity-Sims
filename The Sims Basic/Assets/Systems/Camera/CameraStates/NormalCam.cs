using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCam : CameraState
{
    //Moves around with wasd and click/hold and drag
    //mouse is free and can click on gui/world
    


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

    //If mouse is being tracked for movement
    bool mouseTracked = false;


    //Constructor for normal cam
    public NormalCam(GameObject _camera)
        : base(_camera)
    {
        cameraObject = _camera;
        camera = Camera.main;
    }

    public override void Enter()
    {
        //Event that game is in normal mode

        //Confine mouse to screen 
        Cursor.lockState = CursorLockMode.Confined;

        //Get the camera's anchor point
        anchorPoint = GameManager.Instance.GetCameraAnchorSpawnPos();

        //For now set camera at anchor points height
        //cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, 0, cameraObject.transform.position.z);

        //Move to update loop
        base.Enter();
    }

    public override void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            CanInteract(Input.GetMouseButtonDown();
            //Check if clicked object

            //else drag mouse to move 
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Check if clicked object

            //Else rotate around anchor
        }

        


        //Wasd controls and shift multiplier
        //BasicMovement();

        OrbitAnchor();

        LookAtAnchor();



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
    /// On load camera is zoomed out and eases into anchor
    /// </summary>
    private void MoveToAnchor()
    {

    }


    /// <summary>
    /// Hold right click and rotate around anchor
    /// </summary>
    private void OrbitAnchor()
    {
        //Distance mouse moved
        float mouseDistance = 0;
        //Percentage of screen the mouse moved across
        float movedPercent;
        //Rotation multiplier
        float rotationMultiplier = 500f;


        

        if (Input.GetMouseButtonDown(1))
        {
            mouseOrigin = Input.mousePosition;
            mouseDistance = 0;
        }

        //Holding right click
        if (Input.GetMouseButton(1))
        {
            
            //Set cursor texture
            //for now just hiding it
            Cursor.visible = true;

            //track distance moved from orignal pos
            mouseDistance = Input.mousePosition.x - mouseOrigin.x;

            //Find percentage of screen mouse moved across
            movedPercent = mouseDistance / Screen.width;

            //Rotate camera
            cameraObject.transform.RotateAround(anchorPoint, Vector3.up, movedPercent * Time.deltaTime * rotationMultiplier);

            //Make sure still looking at anchor
            LookAtAnchor();

            //reset mouse orgin
            //mouseOrigin = Input.mousePosition;
        }

        //Stop tracking distance moved
        if (Input.GetMouseButtonUp(1))
        {
            
        }


    }

    /// <summary>
    /// Rotate camera to look at anchor
    /// </summary>
    private void LookAtAnchor()
    {
        cameraObject.transform.LookAt(anchorPoint);
    }


    private void MouseInputs()
    {

        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButtonDown(1))
        {

        }


    }


    /// <summary>
    /// Check if input given would interact with object.
    /// </summary>
    /// <returns>true for can interact</returns>
    private bool CanInteract(KeyCode inputType)
    {
        Debug.Log(inputType);

        //Store a raycaccst
        RaycastHit hit;
        //Ray ray = camera.ScreenPointToRay(Input.MousePosition);

        //if(Physiccs)



        return false;
    }

}
