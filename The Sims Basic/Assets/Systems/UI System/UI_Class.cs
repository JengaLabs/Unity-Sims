using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Class 
{

    public UI_Class(GameObject UI_Object, string name)
    {
        this.thisObject = UI_Object;
        this.name = name;
    }

    


    GameObject thisObject;

    public string name;
    


   

    public void HideAllUI()
    {
        thisObject.SetActive(!thisObject.activeSelf);
    }

    




}
