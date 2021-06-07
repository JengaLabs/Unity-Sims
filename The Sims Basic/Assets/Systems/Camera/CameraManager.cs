using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    //Camera state machine
    CameraState currentState;


    private WorldStates worldStates;



    private void Start()
    {

        GameManager.Instance.GetWorld();
    }

    private void Update()
    {
        
    }


}
