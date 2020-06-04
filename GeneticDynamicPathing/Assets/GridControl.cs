using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    public Button gridButton;
    private Text inputText;
    private List<GameObject> GridPoints { get; set; }
    private List<GameObject> GridLines { get; set; }
    private GameObject TemplatePoint { get; set; }
    private GameObject TemplateDestination { get; set; }
    private GameObject DestinationPoint { get; set; }
    public int DimensionMax { get; set; }
    public int Scale { get; set; } = 2;

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.FindWithTag("GridSize").GetComponent<InputField>().textComponent;
        gridButton.onClick.AddListener(() => CreateGrid());
        GetTemplates();
    }

    private void GetTemplates()
    {
        TemplatePoint = GameObject.FindGameObjectWithTag("TemplatePoint");
        TemplateDestination = GameObject.FindGameObjectWithTag("TemplateDestination");
    }

    //event listener for when the button is clicked
    void CreateGrid()
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
                        CreateGridPoint(x, y, z);
                    }
                }
            }
        }
        DimensionMax = gridRadius;
        //if destination blank or invalid, default to random?
    }

    internal void CreateDestinationPoint(int x, int y, int z)
    {
        DestinationPoint = Instantiate(TemplateDestination);
        Vector3 position = new Vector3(x * Scale, y * Scale, z * Scale);
        DestinationPoint.transform.position = position;
        DestinationPoint.transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
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
        if (GridLines != null && GridLines.Count > 0)
        {
            for (int i = 0; i < GridLines.Count; i++)
            {
                GameObject gridLine = GridLines[i];
                Destroy(gridLine);
            }
        }
        if (DestinationPoint != null)
        {
            Destroy(DestinationPoint);
            DestinationPoint = null;
        }
    }

    internal void CreateGridLine(int startX, int startY, int startZ, int endX, int endY, int endZ)
    {
        //GameObject gridPoint = Instantiate(TemplateLine);

        throw new NotImplementedException();
    }

    private void CreateGridPoint(int x, int y, int z)
    {
        GameObject gridPoint = Instantiate(TemplatePoint);
        Vector3 position = new Vector3(x * Scale, y * Scale, z * Scale);
        gridPoint.transform.position = position;
        gridPoint.transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
        GridPoints.Add(gridPoint);
    }
}
