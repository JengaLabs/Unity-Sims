using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StateCMD
{
    
    public StateCMD(Text textBox)
    {
        commandBox = textBox;
    }

    //text box that has text written
    public Text commandBox;
    
    //Holding the world states
    private Dictionary<string, int> worldStates;



    public void Process(WorldStates states)
    {
       
        //Store the current world states
        worldStates = states.states;
        //Reset the text
        commandBox.text = "";

        foreach (KeyValuePair<string, int> state in worldStates)
        {
            //Find each unique state and write it
            commandBox.text += state.Key + ", " + state.Value + "\n";

        }
       
    }

    

}
