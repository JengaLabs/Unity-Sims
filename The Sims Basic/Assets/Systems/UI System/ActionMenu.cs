using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActionMenu : MonoBehaviour
{
    //Input events from user and game
    private InputClass InputEvents;

    private UnityAction m_firstAction;
    
    //TEMP ACTION STORAGE
    private ObjectActionsStorage _ActionsStoarge;

    //Long term action storage
    private ObjectDataBase _ObjectDataStorage;

    //List of actions for object selected
    private List<string> currentObjectActionNames;

    //List of actaul actions classes
    private List<Action> currentObjectActions = new List<Action>();

    //List of buttons for use
    private List<Button> buttons;

    //Create a queue of buttons
    Queue<Button> Qbuttons;

    //Current object
    private RectTransform thisPosition;

    //Prefab of a action button
    public GameObject actionButton;

    

    private string currentObject = null;

    void Awake()
    {
        //Get Components 
        thisPosition = this.GetComponent<RectTransform>();

        //Get actions storage
        _ActionsStoarge = GameManager.Instance.GetActionsStorage();

        //Get long term object storage
        _ObjectDataStorage = GameManager.Instance.GetObjectDataBase();

        //Get input class
        InputEvents = GameManager.Instance.GetInputClass();

        //subsribe to action menu event
        InputEvents.onClickedObject += OpenActionMenu;

        buttons = new List<Button>();
        Qbuttons = new Queue<Button>();
    }




    private void OpenActionMenu(string objectName)
    {
        InputEvents.PlaySound("Menu_Open_1");
        //When menu is open, it should close when clicking another object that isnt a button
        InputEvents.onClickedObject -= OpenActionMenu;
        InputEvents.onClickedObject += ShutDownActionMenu;

        //Get the objects actions names
        currentObjectActionNames = _ObjectDataStorage.GetActions(objectName);

        //Reset the current object actions
        currentObjectActions.Clear();

        //Loop through every action
        foreach(string name in currentObjectActionNames)
        {
            //add every action by the name
            currentObjectActions.Add(_ActionsStoarge.GetActionByName(name));
        }

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
        Qbuttons.Clear();
    }

    private void AddAllButtonListeners(int numberOfButtons = 10)
    {
        if(buttons.Count <= numberOfButtons)
        {
            buttons.Add(CreateNewButton());
            AddAllButtonListeners();
            return;
        }


        foreach (Button button in buttons)
        {
            Qbuttons.Enqueue(button);
        }





        int moveButton = 0;

        //add actions to each button
        foreach (Action action in currentObjectActions)
        {
            
            //Get the next button
            Button currentButton = Qbuttons.Dequeue();

            //Set that buttons method 
            currentButton.onClick.AddListener(ButtonMethod);
            currentButton.onClick.AddListener(action.CallGActions);
            currentButton.GetComponentInChildren<Text>().text = action._ActionName;
            currentButton.transform.position = currentButton.transform.position + new Vector3(moveButton, 0, 0);
            moveButton += 50;
            currentButton.gameObject.SetActive(true);
        }




        //buttonObj.onClick.AddListener(ButtonMethod);

    }

    private Button CreateNewButton()
    {
        //Instantiate new button
        GameObject newButton = Instantiate(actionButton, this.transform);
        
        //set new buttons transform parrent
        //newButton.transform.parent = this.transform;
        //Add new action button

        return newButton.GetComponent<Button>();
    }

}


