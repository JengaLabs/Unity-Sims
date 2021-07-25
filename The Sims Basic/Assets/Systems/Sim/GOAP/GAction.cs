using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    //actions all have names
    public string actionName = "Action";
    //Cost of the action
    public float cost = 1.0f;
    //Location of objective
    public GameObject target;
    //The tag of the object 
    public string targetTag;
    //Duration of an action
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;

    public NavMeshAgent agent;
    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;



    public GInventory inventory;
    public WorldStates beliefs;


    public bool running = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();

    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();



        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (WorldState a in afterEffects)
            {
                effects.Add(a.key, a.value);
            }
        }

        inventory = this.GetComponent<GAgent>().inventory;
        beliefs = this.GetComponent<GAgent>().beliefs;
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchiebableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> pre in preconditions)
        {
            if (!conditions.ContainsKey(pre.Key))
            {
                return false;
            }


        }

        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();


}
