using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{

    #region Start Game

    #endregion

    #region End Game


    #endregion

    #region Save / Load Game 



    #endregion

    #region Menu Events



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
        if(onPlaySound != null)
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


    #endregion

}
