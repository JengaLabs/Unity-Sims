using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Class 
{

    public UI_Class(GameObject UI_Object)
    {
        this.thisObject = UI_Object;
    }

    


    GameObject thisObject;
    string UI_title;


    private List<GameObject> UIobjects;

    public void HideAllUI()
    {
        foreach(GameObject ui in UIobjects)
        {
            ui.SetActive(!ui.activeSelf);
        }
    }

    public void ShowAllUI()
    {
        foreach (GameObject ui in UIobjects)
        {
            ui.SetActive(!ui.activeSelf);
        }
    }

    private GameObject[] GetChildren()
    {
        GameObject[] tempArray = new GameObject[thisObject.transform.childCount];

        for (int i = 0; i < thisObject.transform.childCount; i++)
        {
            tempArray[i] = thisObject.transform.GetChild(i).gameObject;
        }

        return tempArray;
    }



}
