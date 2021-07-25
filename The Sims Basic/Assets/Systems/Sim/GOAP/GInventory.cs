using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory 
{
    //Items in inventory
    List<GameObject> items = new List<GameObject>();

    /// <summary>
    /// Add a object to inventory
    /// </summary>
    /// <param name="i"></param>
    public void AddItem(GameObject i)
    {
        items.Add(i);
    }

    /// <summary>
    /// Find object in inventory with this tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject i in items)
        {
            if (i.tag == tag)
            {
                return i;
            }
        }

        return null;
    }


    /// <summary>
    /// Remove specific object from inventory
    /// </summary>
    /// <param name="i"></param>
    public void RemoveItem(GameObject i)
    {
        int indexToRemove = -1;

        foreach (GameObject g in items)
        {
            indexToRemove++;
            if (g == i)
            {
                break;

            }
        }

        if (indexToRemove >= -1)
        {
            items.RemoveAt(indexToRemove);
        }

    }
}
