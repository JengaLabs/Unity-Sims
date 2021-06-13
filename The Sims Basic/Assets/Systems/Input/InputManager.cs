using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //MonoBehaviour that tracks input
    //Reports what kind of input to game managers input class delegates



    //Input class that inputs are reported to
    InputClass _InputClass;




    void Start()
    {
        _InputClass = GameManager.Instance.GetInputClass();

        if(_InputClass == null) 
        {
            Debug.Log("Game Manager input class is null");
        }

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse down");
            _InputClass.GUIinput();
        }






    }

    







}
