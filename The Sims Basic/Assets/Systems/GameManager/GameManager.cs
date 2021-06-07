using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sealed class refers that this is non-inheritable aka base class
public sealed class GameManager : MonoBehaviour
{

    //Hold the singleton of game manager
    private static GameManager instance;

    //Class that holds all world states
    private static WorldStates world;


    public static GameManager Instance
    {
        

        get
        {
            //If the singleton doesn't exist, create one
            if(instance == null)
            {
                instance = new GameManager();
                //Perform any code the game manager needs to like creating objects
            }

            return instance;
        }
    }







    /// <summary>
    /// Returns the current world states
    /// </summary>
    /// <returns></returns>
    public WorldStates GetWorld()
    {
        return world;
    }



}
