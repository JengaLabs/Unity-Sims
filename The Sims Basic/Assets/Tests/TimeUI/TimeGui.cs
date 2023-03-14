using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGui : MonoBehaviour
{

    #region Properties 

    [SerializeField] Text _TimeText;

    #endregion

    #region Methods

    private void Awake()
    {
        _TimeText.text = "1x";
    }


    #endregion


    #region Functions

    /// <summary>
    /// Call for increase time scale + 1 
    /// </summary>
    public void IncreaseTime()
    {
        Debug.Log("increase time by 1");
        //Call delegate from time manager to increase time 
    }

    /// <summary>
    /// Call for Decrease time scale - 1
    /// </summary>
    public void DecreaseTimeScale()
    {
        Debug.Log("decrease time by 1");

    }

    /// <summary>
    /// Set time scale to 0 
    /// </summary>
    public void ToggleTime()
    {
        Debug.Log("toggle");

    }

    #endregion







}
