using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputClass 
{

    public InputClass()
    {

    }

    //For when user is typing in a text box
    public delegate void TextBoxInput();
    public TextBoxInput textBoxInput;





    //Delegate for mouse down
    public delegate void OnGuiInput();

    //Variable for delegate
    public OnGuiInput onGuiInput;


    public void GUIinput()
    {

        Debug.Log("Called GUI input sucess");


        //checking for if somthing is subscribed to the event
        if (onGuiInput != null)
        {
            //Script is subscribed than call event
            onGuiInput();
        }
        else
        {
            Debug.Log("Nothing is subscribed");
        }
    }










}
