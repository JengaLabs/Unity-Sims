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
        
        worldStates = GameManager.Instance.GetWorld();
        currentState = new CamPaused(this.gameObject);
    }

    private void Update()
    {
        //Return the updated camera state
        currentState = currentState.Process();





        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (worldStates.HasState("Dead"))
            {
                worldStates.ModifyState("Dead", 1);
            }
            else
            {
                worldStates.AddState("Dead", 1);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (worldStates.HasState("Fun"))
            {
                worldStates.ModifyState("Fun", 1);

            }
            else
            {
                worldStates.AddState("Fun", 1);
            }
        }

        
    }


}
