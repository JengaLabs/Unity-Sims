using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Class for creating goals 
/// </summary>
public class SubGoal
{
    public Dictionary<string, int> SubGoals;
    public bool remove;

    /// <summary>
    /// Give a bot a goal to reach for with the option to remove it
    /// </summary>
    /// <param name="GoalName"></param>
    /// <param name="GoalSize"></param>
    /// <param name="canRemoveGoal"></param>
    public SubGoal(string GoalName, int GoalSize, bool canRemoveGoal)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(GoalName, GoalSize);
        remove = canRemoveGoal;
    }

}

public class GAgent : MonoBehaviour
{

    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();



    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
        {
            actions.Add(a);
        }

    }


    bool invoked = false;

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

     private void LateUpdate()
    {

        if (currentAction != null && currentAction.running && currentAction.target)
        {

            if (currentAction.agent.remainingDistance >= 1f)
            {
                if (currentAction.agent.hasPath == false)
                {
                    Debug.Log("No path to obj returning");
                    return;
                }
            }
            else
            {
                //has goal and has reached it 
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            
            return;
        }
        
        
        

        if (planner == null || actionQueue == null)
        {
            //no plans to work on 
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.SubGoals, beliefs);
                if (actionQueue != null)
                {
                    //have plan
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                
                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }

    }


}
