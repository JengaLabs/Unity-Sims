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


    public void Start()
    {
        _stateCMD = new StateCMD(textbox);
        
        



    }

    private void Update()
    {

        //Debug.Log(GameManager.Instance.GetWorld());
        //World state updated 
        _stateCMD.Process(GameManager.Instance.GetWorld());
        

    }




}
