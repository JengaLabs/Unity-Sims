using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for interactable objects added to world
public class Interactable : MonoBehaviour
{

    //Every interactable has aftereffects
    //When intialized, interactables will add the list of possible actions 
    //a sim can perform on it

    public List<GAction> PossibleActions = new List<GAction>();

    public int SimultaneousUses;


    //object added to a sims queue and should be deselected from potential objects
    private void StartingQueue()
    {

    }

    //Any animations and such that need to happen
    public virtual void StartingInteraction()
    {

    }

    public virtual void EndingInteraction()
    {

    }

    public virtual void FinishedInteraction()
    {

    }

    
    

}
