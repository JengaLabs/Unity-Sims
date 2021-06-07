using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Track and help other classes understand the world states
public class WorldStates 
{
    //Store all the world states 
    public Dictionary<string, int> states;

    //Constructor for world states
    public WorldStates()
    {
        //build world states
        states = new Dictionary<string, int>();
    }

    #region Helper classes
    
    /// <summary>
    /// Does the world contain a given state.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    /// <summary>
    /// Add a new state to the list
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddState(string key, int value)
    {
        //Add the state to world states
        states.Add(key, value);
    }



    /// <summary>
    /// Add or subtract value of a given state.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void ModifyState(string key, int value)
    {
        //Check if state already exist
        if (states.ContainsKey(key))
        {
            states[key] += value;
            //Check if state isnt negative
            if (states[key] <= 0)
            {
                RemoveState(key);
            }
        }
        else
        {
            //Add the state
            states.Add(key, value);
        }
    }

    /// <summary>
    /// Remove a given state.
    /// </summary>
    /// <param name="key"></param>
    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
        else
        {
            Debug.Log("State " + key + " does not exist");
        }
    }

    /// <summary>
    /// Set value of given state. 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            //Creates state if does not exist
            states.Add(key, value);
        }
    }
    
    /// <summary>
    /// Returns states of the game world. 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetStates()
    {
        return states;
    }
    
    
    #endregion

}
