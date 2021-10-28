using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{

    #region Start Game

    public delegate void StartGame();

    public StartGame OnStartGame;


    /// <summary>
    /// Call to load any save files and create and new ones. 
    /// </summary>
    public void StartTheGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame();
        }
        else
        {
            Debug.LogError("Nothing subscribed to load ahead of time.");
        }
    }

    #endregion

    #region End Game


    #endregion

    #region Save / Load Game 

    public delegate void OnSaveDevFiles();

    /// <summary>
    /// Listen for when save methods should be called.
    /// </summary>
    public OnSaveDevFiles OnSave;

    /// <summary>
    /// Call to save current game data
    /// </summary>
    public void SaveGame()
    {
        if (OnSave != null)
        {
            OnSave();
        }
        else
        {
            Debug.LogError("Dev files is not subscribed to save game event.");
        }
    }

    public delegate void OnLoadDevFiles(string fileName);

    public OnLoadDevFiles LoadDevFiles;

    public void LoadGame(string fileName)
    {
        if (LoadDevFiles != null)
        {
            LoadDevFiles(fileName);
        }
        else
        {
            Debug.LogError("Loading subscribers missing");
        }
    }


    #endregion

    #region Menu Events

    ///When a menu needs to be opened or closed.
    public delegate void OnManipulateMenu(string menuName, bool hideStatus);

    /// <summary>
    /// Subscribe to listen to menu call events.
    /// </summary>
    public OnManipulateMenu manipulateMenu;

    /// <summary>
    /// Call a menu by name and open or close it. 
    /// </summary>
    /// <param name="menuName">The menu you want to manipulate.</param>
    /// <param name="hideStatus">If menu should be shown or not.</param>
    public void Manipulate_Menu(string menuName, bool hideStatus)
    {
        //Check for subsribers
        if (manipulateMenu != null)
        {
            manipulateMenu(menuName, hideStatus);
        }
    }

    #endregion

    #region Sound Events

    /// <summary>
    /// Delegate with parameter of type string.
    /// </summary>
    /// <param name="SoundName"></param>
    public delegate void OnPlaySound(string SoundName);

    /// <summary>
    /// Listen for a string of a sound name.
    /// </summary>
    public OnPlaySound onPlaySound;

    public void PlaySound(string soundName)
    {
        if (onPlaySound != null)
        {
            onPlaySound(soundName);
        }
        else
        {
            Debug.LogError("No subscribers to on play sound");
        }
    }

    #endregion

    #region Developer Commands

    public delegate void OnInputString(string input);

    public OnInputString onInputString;

    public void SubmitInputString(string input)
    {
        if (onInputString != null)
        {
            onInputString(input);
        }
        else
        {
            Debug.LogError("No subribers to input string");
        }
    }


    #endregion

    #region Game Manipulation

    #region Select Object 

    //Delegate type that takes a gameObject
    public delegate void SelectObject(GameObject selectedObject);

    /// <summary>
    /// Subscribe and listen to objects that are selected.
    /// </summary>
    public SelectObject onSelectAnObject;

    /// <summary>
    /// Call event with a given object to select it.
    /// </summary>
    /// <param name="selectedObject"></param>
    public void SelectAnObject(GameObject selectedObject)
    {
        //Check for subscribers
        if (onSelectAnObject != null)
        {
            //Call the event with this object
            onSelectAnObject(selectedObject);
        }
        else
        {
            Debug.LogError("Nothing subscribed to onSelectAnObject");
        }
    }

    #endregion

    #region Time Manipulation

    public delegate void SetGameSpeed(int gameSpeed);

    /// <summary>
    /// Listen for changes in game speed
    /// </summary>
    public SetGameSpeed changeGameSpeed;

    /// <summary>
    /// Change the gameSpeed to a given number.
    /// </summary>
    /// <param name="gameSpeed">Game speed to be set as. </param>
    public void ChangeGameSpeed(int gameSpeed)
    {
        if (changeGameSpeed != null)
        {
            changeGameSpeed(gameSpeed);
        }
        else
        {
            Debug.LogError("Nothing subscribed to game speed");
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

    #endregion

    #region User Inputs 

    #region Keyboard


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

    #region Mouse

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


    #endregion

    #endregion

}
