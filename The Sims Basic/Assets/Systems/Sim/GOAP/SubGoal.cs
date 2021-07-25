using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class for creating goals 
/// </summary>
public class SubGoal 
{
    public Dictionary<string, int> SubGoals;
    public bool remove;

    /// <summary>
    /// Give a bot a goal to reach for wit hthe option to remove it 
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
