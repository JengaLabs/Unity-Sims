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

    //List of action for object selected
    private List<Action> currentActions;

    //List of buttons for use
    private List<Button> buttons;


    private RectTransform thisPosition;

    //Prefab of a action button
    public GameObject actionButton;


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

        buttons = new List<Button>();

    }




    private void OpenActionMenu(string objectName)
    {
        InputEvents.PlaySound("Menu_Open_1");
        //When menu is open, it should close when clicking another object that isnt a button
        InputEvents.onClickedObject -= OpenActionMenu;
        InputEvents.onClickedObject += ShutDownActionMenu;

        //Get the objects actions 
        ObjectActions objectActions = _ActionsStoarge.GetObjectActions(objectName);

        //Add actions to list
        currentActions = objectActions.GetActions();


        //Set Action menu active
        this.gameObject.SetActive(true);
        //Move menu to mouse position
        this.thisPosition.anchoredPosition = Input.mousePosition;

        AddAllButtonListeners();


    }


    private void ShutDownActionMenu(string objectName = "")
    {

        InputEvents.PlaySound("Menu_Close_2");


        //Set object as inactive
        this.gameObject.SetActive(false);
        //Resubscribe to the opening event
        InputEvents.onClickedObject += OpenActionMenu;
        InputEvents.onClickedObject -= ShutDownActionMenu;

        //Remove any action listenrs
        RemoveAllButtonsListeners();
    }

    /// <summary>
    /// For closing menu after clicking a action
    /// </summary>
    /// <param name="objectName"></param>
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
            InputEvents.onClickedObject -= ShutDownActionMenu;

            //Remove any action listenrs
            RemoveAllButtonsListeners();


        }
        else
        {

            return;
        }

    }


    private void ButtonMethod()
    {
        //Call close menu sound
        InputEvents.PlaySound("Menu_Close_1");

        //Close the menu 
        CloseActionMenu("close");

        //For now just call the action of that button
        //currentActions[0].CallGActions();


    }

    private void RemoveAllButtonsListeners()
    {
        //Make all buttons stop listening for events
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void AddAllButtonListeners()
    {
        //Create a queue of buttons
        Queue<Button> Qbuttons = new Queue<Button>();

        //Check if more buttons are needed
        for (int i = currentActions.Count - buttons.Count; i > 0; i = currentActions.Count - buttons.Count)
        {
            CreateNewButton();
        }

        //Go through each button and enqueue them
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
            Qbuttons.Enqueue(button);
        }

        //add actions to each button
        foreach (Action action in currentActions)
        {
            //Get the next button
            Button currentButton = Qbuttons.Dequeue();

            //Set that buttons method 
            currentButton.onClick.AddListener(ButtonMethod);
            currentButton.onClick.AddListener(action.CallGActions);
            currentButton.GetComponentInChildren<Text>().text = action._ActionName;
            currentButton.gameObject.SetActive(true);
        }




        //buttonObj.onClick.AddListener(ButtonMethod);

    }

    private void CreateNewButton()
    {
        //Instantiate new button
        GameObject newButton = Instantiate(actionButton, this.transform);
        
        //set new buttons transform parrent
        //newButton.transform.parent = this.transform;
        //Add new action button
        buttons.Add(newButton.GetComponent<Button>());

    }

}


