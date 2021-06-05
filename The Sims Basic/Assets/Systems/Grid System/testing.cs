using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{

    Grid grid;

    int value;

    private void Start()
    {
        grid = new Grid(5, 10, 5, this.transform.position);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), 10);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }



}
