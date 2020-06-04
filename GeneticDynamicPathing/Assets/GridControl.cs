using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    public Button gridButton;
    private Text inputText;
    private List<GameObject> GridPoints { get; set; }
    private GameObject TemplatePoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.FindWithTag("GridSize").GetComponent<InputField>().textComponent;
        gridButton.onClick.AddListener(() => CreateGrid(2));
        GetTemplatePoint();
    }

    private void GetTemplatePoint()
    {
        TemplatePoint = GameObject.FindGameObjectWithTag("TemplatePoint");
    }

    //event listener for when the button is clicked
    void CreateGrid(int scale)
    {
        string sizeText = inputText.text; //TODO: Add size limit
        int gridRadius = int.Parse(sizeText);

        DestroyGrid();
        GridPoints = new List<GameObject>();

        for (int x = -gridRadius; x <= gridRadius; x++) //Creates a grid around origin
        {
            for (int y = -gridRadius; y <= gridRadius; y++)
            {
                for (int z = -gridRadius; z <= gridRadius; z++)
                {
                    if (!(x == 0 && y == 0 && z == 0))
                    {
                        CreateGridPoint(x, y, z, scale);
                    }
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
        GameObject gridPoint = Instantiate(TemplatePoint);
        Vector3 position = new Vector3(x * scale, y * scale, z * scale);
        gridPoint.transform.position = position;
        gridPoint.transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
        GridPoints.Add(gridPoint);
    }
}
