using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{

    //Store a Ui name and booleon for on/oof
    public Dictionary<string, bool> UIoptions;

    //Store the world states 
    private WorldStates worldStates;

    


    public void Start()
    {
        
    }

    private void Update()
    {
        //Get the current world states
        worldStates = GameManager.Instance.GetWorld();



    }




}
