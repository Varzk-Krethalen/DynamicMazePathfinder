using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    public Button gridButton;
    private List<GameObject> GridPoints { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gridButton.onClick.AddListener(() => CreateGrid(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid(int scale)
    {
        string sizeText = GameObject.FindWithTag("GridSize").GetComponent<InputField>().textComponent.text; //TODO: Add size limit
        int gridSize = int.Parse(sizeText);

        Debug.Log(GridPoints?.Count);

        DestroyGrid();
        GridPoints = new List<GameObject>();
        int gridRadius = gridSize / 2;
        int gridAntiPadding = gridSize % 2 == 0 ? 1 : 0;
        for (int x = -gridRadius + gridAntiPadding; x <= gridRadius; x++) //Creates a grid around origin
        {
            for (int y = -gridRadius + gridAntiPadding; y <= gridRadius; y++)
            {
                for (int z = -gridRadius + gridAntiPadding; z <= gridRadius; z++)
                {
                    CreateGridPoint(x, y, z, scale);
                }
            }
        }
        //if destination blank or invalid, default to random
        //origin always 0,0,0?
        //set origin to red, destination to green?
    }

    private void DestroyGrid()
    {
        if (GridPoints != null && GridPoints.Count > 0)
        {
            for (int i = 0; i < GridPoints.Count; i++)
            {
                GameObject gridPoint = GridPoints[i];
                Destroy(gridPoint);
            }
        }
    }

    private void CreateGridPoint(int x, int y, int z, int scale)
    {
        GameObject gridPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 position = new Vector3(x * scale, y * scale, z * scale);
        gridPoint.transform.position = position;
        gridPoint.transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
        GridPoints.Add(gridPoint);
    }
}
