using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Have to use to detect if hit ui or gameobject
using UnityEngine.EventSystems;

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
    /// Use for getting and setting events. 
    /// </summary>
    public GameEventManager gameEventManager;

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

    /// <summary>
    /// Camera zoom level.
    /// </summary>
    public float _CurrentZoomLevel = 10f;

    //Zoom limits
    public float farZoomLimit = 22f;
    public float nearZoomLimit = 1f;

    //That only cast rays against colliders in layer 2
    //Bit shift the index of the layer (2) to get a bit mask
    int layerMask = 1 << 2;

    public CameraState(GameObject _camera)
    {
        //Don't cast on second layer
        layerMask = ~layerMask;


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
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverInteractable())
            {
            }
        }

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
    /// Rotate the camera object to look at the anchor.
    /// </summary>
    /// <param name="CameraObject">Actual Camera</param>
    /// <param name="AnchorPosition">The position of the anchor.</param>
    public void CameraLookAtAnchor(GameObject CameraObject, Vector3 AnchorPosition)
    {
        CameraObject.transform.LookAt(AnchorPosition);
    }


    /// <summary>
    /// Scroll in and out from the camera anchor
    /// </summary>
    /// <param name="CameraObject"></param>
    public void LocalSmoothScroll(GameObject CameraObject)
    {
        //Get scroll wheel input
        float scrollDistance = Input.mouseScrollDelta.y;
        //Take from current scroll distance level
        _CurrentZoomLevel -= scrollDistance;
        //Clamp the zoom value 
        _CurrentZoomLevel = Mathf.Clamp(_CurrentZoomLevel, nearZoomLimit, farZoomLimit);

        Vector3 newPos = new Vector3(CameraObject.transform.localPosition.x, Mathf.Pow(1.2f, _CurrentZoomLevel), -_CurrentZoomLevel * 1.2f);

        //Check user scrolled
        if (scrollDistance != 0)
        {
            //Scroll is inbound
            if (_CurrentZoomLevel < farZoomLimit && _CurrentZoomLevel > nearZoomLimit)
            {
                //Debug.Log("Current zoom level is " + _CurrentZoomLevel);
                //Move camera to new position
                CameraObject.transform.localPosition = newPos;
            }
        }

        //Reset view
        CameraLookAtAnchor(CameraObject, CameraObject.transform.parent.position);
    }


    public bool IsMouseOverInteractable()
    {

        #region Check for UI that was hit 

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);



        //Loop over what UI was hit
        for (int i = 0; i < raycastResults.Count; i++)
        {
            //switch statement for what to do for each layer
            switch (raycastResults[i].gameObject.layer)
            {
                //Check for game layer
                case 0:
                    //Debug.Log(raycastResults[i].gameObject.name + " this is nothing");
                    break;
                case 2:
                    //This is ignored

                    //Debug.Log(raycastResults[i].gameObject.name + " this is ignored");
                    break;
                case 5:
                    //Debug.Log(raycastResults[i].gameObject.name + " is a ui");
                    //Call the interactable script 
                    gameEventManager.SelectAnObject(raycastResults[i].gameObject);
                    return true;


            }


        }

        #endregion

        #region Check for gameobjects that were hit 

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer 
        if (Physics.Raycast(ray, out hit, 100f))
        {

            //Debug.Log(hit.transform.gameObject.layer);
            switch (hit.transform.gameObject.layer)
            {
                case 0:
                    //Debug.Log(hit.transform.gameObject.name + " this is enviroment");

                    //Call left clicked enviroment event 
                    //_InputClass.leftClickedEnviroment(hit.point);
                    //Call left clicked event for action menu class

                    


                    return false;

                case 8:

                    Debug.Log(hit.transform.gameObject.name + " this is an interactable");

                    /*
                    SubGoal s3 = new SubGoal("Sitting", 1, true);
                    Sim mysim = GameManager.Instance.GetSelectedSim();
                    mysim.AddGoal(s3);
                    */
                    
                    

                    gameEventManager.SelectAnObject(hit.transform.gameObject);

                    //Set object as selected one
                    return true;

            }
        }
        #endregion


        return false;

    }

}
