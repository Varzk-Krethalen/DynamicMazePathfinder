using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    public Shader lineShader;
    public Button gridButton;
    private Text inputText;
    private Text inputScale;
    private List<GameObject> GridPoints { get; set; }
    private List<GameObject> GridLines { get; set; }
    private GameObject TemplatePoint { get; set; }
    private GameObject TemplateDestination { get; set; }
    private GameObject DestinationPoint { get; set; }
    public int DimensionMax { get; set; }
    private int Scale { get; set; } = 2; //TODO - add UI option?
    private Vector3 ObjectScale { get; set; } = new Vector3((float)0.25, (float)0.25, (float)0.25);

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.FindWithTag("GridSize").GetComponent<InputField>().textComponent;
        inputScale = GameObject.FindWithTag("Scale").GetComponent<InputField>().textComponent;

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
        string sizeText = inputText.text;
        int gridRadius = int.Parse(sizeText);

        string scaleText = inputScale.text;
        try
        {
            Scale = int.Parse(scaleText);
        }
        catch
        {
            Scale = 2;
        }

        ResetGridPoints();
        ResetDestinationPoint();
        ResetLines();
        GridPoints = new List<GameObject>();
        GridLines = new List<GameObject>();

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
    }

    internal void CreateDestinationPoint(int x, int y, int z)
    {
        DestinationPoint = Instantiate(TemplateDestination);
        DestinationPoint.transform.position = GetGridScaledVector(x, y, z);
        DestinationPoint.transform.localScale = ObjectScale;
    }

    internal void ResetDestinationPoint()
    {
        if (DestinationPoint != null)
        {
            Destroy(DestinationPoint);
            DestinationPoint = null;
        }
    }

    internal void ResetGridPoints()
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

    internal void ResetLines()
    {
        if (GridLines != null && GridLines.Count > 0)
        {
            for (int i = 0; i < GridLines.Count; i++)
            {
                GameObject gridLine = GridLines[i];
                Destroy(gridLine);
            }
        }
    }

    //based on http://answers.unity.com/answers/1108340/view.html
    internal void CreateGridLine(int startX, int startY, int startZ, int endX, int endY, int endZ)
    {
        Color color = new Color(0.505f, 0.145f, 0.552f);
        Vector3 start = GetGridScaledVector(startX, startY, startZ);
        Vector3 end = GetGridScaledVector(endX, endY, endZ);

        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Standard"));
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = 0.2f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        GridLines.Add(myLine);
    }

    private void CreateGridPoint(int x, int y, int z)
    {
        GameObject gridPoint = Instantiate(TemplatePoint);
        gridPoint.transform.position = GetGridScaledVector(x, y, z);
        gridPoint.transform.localScale = ObjectScale;
        GridPoints.Add(gridPoint);
    }

    private Vector3 GetGridScaledVector(int x, int y, int z)
    {
        return new Vector3(x * Scale, y * Scale, z* Scale);
    }
}
