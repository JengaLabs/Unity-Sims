using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputClass 
{

    public InputClass()
    {

    }

    #region Nothing clicked delegate

    /// <summary>
    /// Delegate for when nothing is being clicked 
    /// </summary>
    public delegate void ClickedNothing();
    //Delegate for objects to subscribe to 
    public ClickedNothing onNothingClicked;
    /// <summary>
    /// Call on nothing clicked delegate
    /// </summary>
    public void CallClickedNothing()
    {
        //Check for subscribers
        if(onNothingClicked != null)
        {
            onNothingClicked();
        }
    }

    #endregion



    //left click, returns if what was hit is a gui or clickable
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



    /// <summary>
    /// Return the object selected by selected 
    /// </summary>
    /// <returns></returns>
    public GameObject GetSelected()
    {
        return _Selected;
    }







}
