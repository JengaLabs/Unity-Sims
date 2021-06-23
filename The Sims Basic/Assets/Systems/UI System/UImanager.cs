using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{

    //Store a Ui name and booleon for on/oof
    public Dictionary<string, bool> UIoptions;

   
    private StateCMD _stateCMD;

    public Text textbox;

    //Input class that input events are reported to 
    InputClass _InputClass;

    public GameObject[] gameObjects = null;


    public void Start()
    {
        gameObjects = GetChildren();
        
        _stateCMD = new StateCMD(textbox);

        //Get the input manager
        _InputClass = GameManager.Instance.GetInputClass();

        //subscribe to input classes
        _InputClass.onTogglePause += HideAllUI;


    }

    private void Update()
    {

        //Debug.Log(GameManager.Instance.GetWorld());
        //World state updated 
        _stateCMD.Process(GameManager.Instance.GetWorld());
        
       
    }

    private GameObject[] GetChildren()
    {
        GameObject[] tempArray = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            tempArray[i] = this.transform.GetChild(i).gameObject;
        }

        return tempArray;
    }



    public void HideAllUI()
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }




}
