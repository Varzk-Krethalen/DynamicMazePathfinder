using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    public Button gridButton;
    private List<GameObject> GridPoints { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gridButton.onClick.AddListener(() => CreateGrid(5,5,5,3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid(int xLength, int yLength, int zLength, int scale)
    {
        //get grid params here instead?
        GridPoints = new List<GameObject>();
        xLength = xLength / 2;
        yLength = yLength / 2;
        zLength = zLength / 2;
        for (int x = -xLength; x <= xLength; x++) //Creates a grid around origin
        {
            for (int y = -yLength; y < yLength; y++)
            {
                for (int z = -zLength; z < zLength; z++)
                {
                    CreateGridPoint(x, y, z, scale);
                }
            }
        }
        //if destination blank or invalid, default to random
        //origin always 0,0,0?
        //set origin to red, destination to green?
    }

    private void CreateGridPoint(int x, int y, int z, int scale)
    {
        GameObject gridPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gridPoint.transform.position.Set(x*scale, y*scale, z*scale);
        GridPoints.Add(gridPoint);
    }
}
