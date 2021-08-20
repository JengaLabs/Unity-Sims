using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActionMenu : MonoBehaviour
{
    private InputClass InputEvents;

    private UnityAction m_firstAction;

    private ObjectActionsStorage _ActionsStoarge;

    private List<Action> currentActions;
    private RectTransform thisPosition;
    public Text buttonText;
    public Button buttonObj;

    private string currentObject = null;
    
    void Awake()
    {
        //Get Components 
        thisPosition = this.GetComponent<RectTransform>();

        //Get action storage
        _ActionsStoarge = GameManager.Instance.GetActionsStorage();

        //Get input class
        InputEvents = GameManager.Instance.GetInputClass();

        //subsribe to action menu event
        InputEvents.onClickedObject += OpenActionMenu;

        
    }


    

    private void OpenActionMenu(string objectName)
    {
        //When menu is open, it shouldnt reopen upon clicking another object.
        InputEvents.onClickedObject -= OpenActionMenu;
        InputEvents.onClickedObject += CloseActionMenu;

        //Get the objects actions 
        ObjectActions objectActions = _ActionsStoarge.GetObjectActions(objectName);

        //Add actions to list
        currentActions = objectActions.GetActions();
        

        //Set Action menu active
        this.gameObject.SetActive(true);
        //Move menu to mouse position
        this.thisPosition.anchoredPosition = Input.mousePosition;

        //Set text to that objects name
        //buttonText.text = objectName;
        //
        //m_firstAction = new UnityAction(currentActions[0].CallGActions);

        //buttonObj.onClick.AddListener(m_firstAction);
        buttonObj.onClick.AddListener(ButtonMethod);

        
        
    }

    
    private void CloseActionMenu(string objectName = "")
    {
        //Check if object same object was already selected
        /*
            IMPORTANT
        this will not work in long run as gameobjects sharing names will be considered different
        */
        if (objectName != currentObject || objectName == "close") 
        {
            //Set object as inactive
            this.gameObject.SetActive(false);
            //Resubscribe to the opening event
            InputEvents.onClickedObject += OpenActionMenu;
            InputEvents.onClickedObject -= CloseActionMenu;

            RemoveAllButtonsListeners();
        }
        else
        {
            return;
        }

    }

        
    private void ButtonMethod()
    {
        
        //Close the menu and call the method for this specific button
        CloseActionMenu("close");

        //For now just call the first action
        currentActions[0].CallGActions();

        
    }

    private void RemoveAllButtonsListeners()
    {
        //Make all buttons stop listening for events
        buttonObj.onClick.RemoveAllListeners();
    }


}


