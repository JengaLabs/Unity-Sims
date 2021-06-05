using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    #region Description
    /*
        The main camera has multiple states.
    
    Menu State:in which the camera is frozen in place and movement input does nothing.
    Free Roam State: Camera can zoom in and out with scroll wheel, move with WASD / left click and drag

    */
    #endregion

    //Keep a reference to the camera 
    private Camera MainCamera;


    


    private void Awake()
    {
        MainCamera = this.GetComponent<Camera>();
    }

    


}
