using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Have to use to detect if hit ui or gameobject
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    //MonoBehaviour that tracks input
    //Reports what kind of input to game managers input class delegates



    //Input class that input events are reported to 
    InputClass _InputClass;

    /// <summary>
    /// Stores the main camera for future uses
    /// </summary>
    Camera mainCamera;


    void Start()
    {
        _InputClass = GameManager.Instance.GetInputClass();
        mainCamera = Camera.main;


        if(_InputClass == null) 
        {
            Debug.Log("Game Manager input class is null");
        }

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            //Check if mouse down was on a UI 
            if (!IsMouseOverInteractable())
            {
                //Nothing is interactable 
                _InputClass.CallClickedNothing();
            }
            else
            {
                //Call interactable for object selected 
                Debug.Log("Mouse down");
                _InputClass.GUIinput();

            }


            
        }



        if (Input.GetMouseButtonDown(0))
        {
            //Check what was hit
           
            
        }




    }


    private bool IsMouseOverInteractable()
    {


        #region Check for UI that was hit 

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        //Loop over what was hit
        for (int i = 0; i < raycastResults.Count; i++)
        {
            //switch statement for what to do for each layer 
            Debug.Log(raycastResults[i]);
        }

        #endregion

        #region Check for gameobject that was hit 



        return false;

    }
    







}
