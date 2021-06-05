using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    //Store the sizes of the grid
    private int width;
    private int length;

    //Length width and height of each cell
    private float cellSize;

    //Starting posistion of the grid
    private Vector3 originPosition;
    
    //Store the grid positions
    private int[,] gridArray;
    //Store grid debug text
    private TextMesh[,] debugTextArray;


    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.length = height;
        this.cellSize = cellSize;

        gridArray = new int[width, this.length];

        debugTextArray = new TextMesh[width, height];

        this.originPosition = originPosition;

        for (int x = 0; x<gridArray.GetLength(0); x++)
        {
            for(int z = 0; z<gridArray.GetLength(1); z++)
            {
                //Debug.Log(x + "," + z);
                debugTextArray[x,z] = CreateWorldText(null, "(" + x + "," + z + ")" , GetWorldPosition(x, z) + new Vector3(cellSize, cellSize, cellSize) * .5f, 8, Color.black, TextAnchor.MiddleCenter, TextAlignment.Center);

                //Draw the grid Z axis 
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.blue, 100f);
                //Draw the grid X axis
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.red, 100f);

                
            }
        }


        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        SetValue(0, 4, 58);
    
    }



    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    public void SetValue(int x, int z, int value)
    {
        

        if (x >= 0 && z >= 0 && x < width && z < this.length)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        Debug.Log(x + ", " + z);
        SetValue(x, z, value);
    }

    public int GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < this.length)
        {
            return gridArray[x, z];
        }
        else
        {
            return 0;
        }
    }


    public int GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        return GetValue(x, z);
    }
    public void GetXY(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }



    public TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }






}
