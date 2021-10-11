using UnityEngine;

public class InputClass
{

    public Vector3 StoredLocation;
    public GameObject StoredObject;

    public InputClass()
    {

    }

    #region Nothing clicked delegate

    /// <summary>
    /// Delegate for when nothing is being clicked 
    /// </summary>
    public delegate void ClickedNothing();
    //Delegate for objects to subscribe to 
    public ClickedNothing onNothingRightClicked;
    /// <summary>
    /// Call on nothing clicked delegate
    /// </summary>
    public void CallClickedNothing()
    {
        //Check for subscribers
        if (onNothingRightClicked != null)
        {
            onNothingRightClicked();
        }
    }

    #endregion

    #region Left Clicked GUI

    //left click, returns bool if what was hit is a gui or clickable
    public delegate bool leftClickedGUI();

    private GameObject _Selected;

    //Delegate for mouse down
    public delegate void OnGuiInput();

    //Variable for delegate
    public OnGuiInput onGuiInput;


    public void GUIinput()
    {

        Debug.Log("Called GUI input success");


        //checking for if somthing is subscribed to the event
        if (onGuiInput != null)
        {
            //if Script is subscribed than call event
            onGuiInput();
        }
        else
        {
            Debug.Log("Nothing is subscribed");
        }
    }
    #endregion

    #region Left Clicked Enviroment
    //When the user left clicks the enviroment
    public delegate void OnLeftClickedEnviroment(Vector3 selectedPoint);

    private Vector3 pointClicked;

    //Varible for delgate to listen to
    public OnLeftClickedEnviroment onLeftClickedEnviroment;

    public void leftClickedEnviroment(Vector3 selectedPoint)
    {
        Debug.Log("Left clicked enviroment");

        //Check if anything is subscribed
        if (onLeftClickedEnviroment != null)
        {
            //Call event with selected point
            onLeftClickedEnviroment(selectedPoint);
        }
        else
        {
            Debug.Log("Nothing is subscribed");
        }

    }
    #endregion

    #region Left Clicked Object

    //When user clicks a game object
    public delegate void OnLeftClickedGameObject(string objectName);

    //Variable for delagte to listen to
    public OnLeftClickedGameObject onClickedObject;

    //Method to call when clicked object
    public void leftClickedGameObject(string _objectName)
    {
        //Check for subscribers
        if (onClickedObject != null)
        {
            //Call event with string name
            onClickedObject(_objectName);
        }
        else
        {
            Debug.LogError("Nothing is subscribed to left clicked game object");
        }
    }

    #endregion

    #region Espace button       
    /// <summary>
    /// Delegate that calls when the button escape is pressed
    /// </summary>
    public delegate void EscapeButton();
    /// <summary>
    /// Delegate to subscribe to when listening for escape button
    /// </summary>
    public EscapeButton onEscapeButton;

    public void EscapeButtonDown()
    {
        //check for subscribers
        if (onEscapeButton != null)
        {
            onEscapeButton();
        }
    }


    #endregion

    #region Toggle Pause

    /// <summary>
    /// Delegate that calls when game needs to toggle state
    /// </summary>
    public delegate void TogglePause();
    /// <summary>
    /// Delegate to sbuscribe to when listening for pause / resume
    /// </summary>
    public TogglePause onTogglePause;

    public void CallTogglePause()
    {
        //Check for subscribers
        if (onTogglePause != null)
        {
            onTogglePause();
        }
        else
        {
            Debug.Log("Nothing subscribed to toggle pause");
        }
    }
    #endregion
    
    /// <summary>
    /// Return the object selected by selected 
    /// </summary>
    /// <returns></returns>
    public GameObject GetSelected()
    {
        return _Selected;
    }


    #region Sim Related Delegates  

    public delegate void GiveLocationGoal(Vector3 locationGoal);







    #endregion

    #region Menu Related Actions 

    //When a menu needs to be opened or hidden
    public delegate void OnManipulateMenu(string menuName, bool hideStatus);

    //Variable for delagate to listen to
    public OnManipulateMenu manipulateMenu;

    //Method to call when wanting to manipulate menu
    public void ManipulateAMenu(string menuName, bool hideStatus)
    {
        //check for subscribers
        if(manipulateMenu != null)
        {
            manipulateMenu(menuName, hideStatus);
        }
        else
        {
            Debug.LogError("Menu managers not subscribed to manipulate a menu");
        }
    }


    #endregion

    #region Sound Events
    //When a script wants to call a sound by name
    public delegate void OnPlaySound(string SoundName);

    //Variable for listeners
    public OnPlaySound onPlaySound;

    public void PlaySound(string soundName)
    {
        if (onPlaySound != null)
        {
            onPlaySound(soundName);
        }
        else
        {
            Debug.Log("No subscribers to on play sound");
        }
    }

    #endregion

    #region Save Event

    //When game object data needs to be saved.
    public delegate void OnSaveDevFiles();

    public OnSaveDevFiles OnSave;

    public void SaveGame()
    {
        if(OnSave != null)
        {
            OnSave();
        }
        else
        {
            Debug.LogError("Dev files is not subscribed to save game event.");
        }
    }


    #endregion

    #region InputString

    //When user inputs a string 
    public delegate void OnInputString(string input);

    //Variable for delegates to listen too
    public OnInputString onInputString;

    public void SubmitInputString(string input)
    {
        if(onInputString != null)
        {
            onInputString(input);
        }
        else
        {
            Debug.Log("No listeners to input string");
        }
    }


    #endregion

    #region Open Developer Console

    //When the developer console needs to be opened
    public delegate void OnOpenDevloperConsole();

    //Variable to subsribe to
    public OnOpenDevloperConsole onDeveloperOpenConsole;

    //Method to call 
    public void ToggleDevloperMenu()
    {
        //check for subscribers
        if (onDeveloperOpenConsole != null)
        {
            //call subsribers
            onDeveloperOpenConsole();
        }
        else
        {
            Debug.LogError("Nothing subscribed to console");
        }
    }


    #endregion


}
