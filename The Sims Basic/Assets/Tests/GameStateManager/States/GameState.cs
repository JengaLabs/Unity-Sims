using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CameraState;

public class GameState
{


    #region Properties
    //Enum hold all the game staes
    public enum Game_State
    {
        PeacefulCam, //Standard sims gameplay
        InMenu, //Menu that doesn't allow the player to move or interact with gameplay menus
        FreeCam, //Dev camera to get close up
    }

    //Progress through states or the current Act
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }


    //Holds current states name for debuging
    public Game_State stateName;
    //Act of the state
    protected EVENT stage;
    //Store the next state 
    public GameState nextState;
    //The actual camera
    protected GameObject playerCamera;
    #endregion

    #region Game state methods

    public GameState(GameObject _PlayerCamera)
    {
        this.playerCamera = _PlayerCamera;

    }

    #endregion


    #region Helper methods

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
                    // gameEventManager.SelectAnObject(raycastResults[i].gameObject);
                    return true;


            }


        }
        #endregion


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
                    return true;

            }
        }


        return false;


    }



    #endregion

}
