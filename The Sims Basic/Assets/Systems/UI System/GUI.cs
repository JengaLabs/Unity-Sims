using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour, IPooledGui
{
    
    public void OnGuiPooled()
    {
        Debug.Log("Hi");
    }
}