using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Have to use to detect if hit ui or gameobject
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    //MonoBehaviour that tracks input
    //Reports what kind of input to game managers input class delegates


    //private GameObject selected = null;



    //Input class that input events are reported to 
    InputClass _InputClass;

    /// <summary>
    /// Stores the main camera for future uses
    /// </summary>
    Camera mainCamera;


    //Bit shift the index of the layer (2) to get a bit mask
    int layerMask = 1 << 2;
    //That only cast rays against colliders in layer 2

    



    void Start()
    {
        //if you want to check everthing but the declared layer use
        layerMask = ~layerMask;

        
        mainCamera = Camera.main;

        _InputClass = GameManager.Instance.GetInputClass();
        if (_InputClass == null) 
        {
            //Debug.Log("Game Manager input class is null");
        }

    }

    void Update()
    {


        #region Mouse Inputs 

        //Right clicked
        if (Input.GetMouseButtonDown(1))
        {

            _InputClass.CallClickedNothing();



        }


        //Left clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Check what was hit
            //Check if mouse down was on a UI 
            if (IsMouseOverInteractable())
            {
                //Call interactable for object selected 
                //Debug.Log("Mouse down");
                _InputClass.GUIinput();

            }
            
            
        }

        #endregion

        //Escape button pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _InputClass.EscapeButtonDown();
        }

        


    }
    
    private void test()
    {
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }


    private bool IsMouseOverInteractable()
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
                    return true;
                    
                
            }


        }

        #endregion

        #region Check for gameobjects that were hit 

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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

                    //Store location clicked and object 
                    _InputClass.StoredLocation = hit.point;
                    _InputClass.StoredObject = hit.transform.gameObject;

                    //Pass the enviroment game object 
                    _InputClass.leftClickedGameObject("Enviroment");

                    
                    
                    return false;
                    
                case 8:
                    
                    Debug.Log(hit.transform.gameObject.name + " this is an interactable");

                    SubGoal s3 = new SubGoal("Sitting", 1, true);
                    Sim mysim = GameManager.Instance.GetSelectedSim();
                    mysim.AddGoal(s3);

                    //Set object as selected one
                    return true;
                    
            }
        }
        #endregion


        return false;

    }
    
    public void TogglePauseGame()
    {
        _InputClass.CallTogglePause();
    }

    







}
