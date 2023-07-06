using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiPool : MonoBehaviour
{

    public Transform canvas; 

    //Class of a pool
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    //pool of unity ui objects 
    public Dictionary<string, Queue<GameObject>> poolDictionary;



    void Start()
    {

        if(canvas == null)
        {
            Debug.Log("Canvas not found");
            return;
        }


        Vector2 p1 = Vector2.zero;
        Vector2 p2 = Vector2.zero;


        //Dictionary of pools
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Add pools to dictionary
        foreach (Pool pool in pools)
        {
            //add object to poll
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //Create max amount for each pool
            for (int i = 0; i < pool.size; i++)
            {
                //create ui object
                GameObject obj = Instantiate(pool.prefab);
                //hide ui
                obj.SetActive(false);
                //Set parent
                obj.transform.SetParent(this.transform);
                //Add to queue
                objectPool.Enqueue(obj);
            }

            //add pool to dictionary 
            poolDictionary.Add(pool.tag, objectPool);

        }


        //SpawnFromPool("Background", canvas, new Vector2(0f, 0f), new Vector2(1f, 1f));

    }

    
    
    public GameObject SpawnFromPool (string tag, Transform parent, Vector2 minPos, Vector2 maxPos)
    {

        //Check if tag exist
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        //Get that game object from the list
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //Customize that gameobject
        //Set active
        objectToSpawn.SetActive(true);
        //Set parent transform 
        objectToSpawn.transform.SetParent(parent);
        //Get rect transform
        RectTransform size = objectToSpawn.GetComponent(typeof(RectTransform)) as RectTransform;
        //Set position
        size.offsetMin = new Vector2(0, 0);
        size.offsetMax = new Vector2(0, 0);
        //Set anchors 
        size.anchorMax = maxPos;
        size.anchorMin = minPos;

        //Re queue
        //poolDictionary[tag].Enqueue(objectToSpawn);

        //Return the new ui
        return objectToSpawn;
    }

    
}
