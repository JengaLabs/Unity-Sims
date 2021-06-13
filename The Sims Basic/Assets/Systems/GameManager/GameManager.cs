using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sealed class refers that this is non-inheritable aka base class
public sealed class GameManager 
{

    //Hold the singleton of game manager
    private static GameManager instance = new GameManager();

    //Class that holds all world states
    private static WorldStates world = new WorldStates();

    //Hold the spawn point of the world
    private static Vector3 CameraAnchorPoint = new Vector3(0, 5, 0);


    public static GameManager Instance
    {
        

        get
        {
            //If the singleton doesn't exist, create one
            if(instance == null)
            {
                instance = new GameManager();

                //Perform any code the game manager needs to like creating objects
                world = new WorldStates();
            
            
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

    public Vector3 GetCameraAnchorSpawnPos()
    {
        return CameraAnchorPoint;
    }



}
