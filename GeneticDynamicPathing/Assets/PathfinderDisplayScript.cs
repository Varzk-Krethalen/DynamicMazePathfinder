using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DynamicPathfinder;
using System;
using System.Linq;

//[InitializeOnLoad]
public class PathfinderDisplayScript : MonoBehaviour
{
    private GeneticAlgorithm PathFinder { get; set; }
    private GridControl GridController { get; set; }
    private int NumberOfGenomes { get; set; }
    private int IterationsPerGeneration { get; set; }
    private float IterationPeriod { get; set; } = 0.1f;
    private Coordinate LastLinePoint { get; set; }
    private Coordinate OriginPoint { get; set; }
    private Coordinate DestinationPoint { get; set; }
    private bool running = false;
    private int lastGeneration;

    public void RunPathfinder()
    {
        UpdateGridController();

        if (!running && UpdateSettings())
        {
            lastGeneration = 0;
            PathFinder = new GeneticAlgorithm(NumberOfGenomes, IterationsPerGeneration, OriginPoint, DestinationPoint);
            UpdateDestinationOnGrid();
            LastLinePoint = OriginPoint;
            SetStatus("Running");
        }
        else
        {
            SetStatus("Invalid settings");
            return;
        }
        running = !running;
        if (!running)
        {
            SetStatus("Stopped");
        }
    }

    private static void SetStatus(string statusString)
    {
        GameObject.FindWithTag("Status").GetComponent<Text>().text = statusString;
    }

    private bool UpdateSettings() //TODO: get from field, TODO: alter mutation rate/crossover percent
    {
        OriginPoint = new Coordinate(0, 0, 0);
        DestinationPoint = new Coordinate(GridController.DimensionMax, GridController.DimensionMax, GridController.DimensionMax); //TODO: make random if not user defined
        var iterationsText = GameObject.FindWithTag("Iterations").GetComponent<InputField>().textComponent;
        if (iterationsText != null && !iterationsText.text.Equals(string.Empty))
        {
            IterationsPerGeneration = int.Parse(iterationsText.text);

            var genomesText = GameObject.FindWithTag("Genomes").GetComponent<InputField>().textComponent;
            if (TextObjectIsValid(genomesText))
            {
                NumberOfGenomes = int.Parse(genomesText.text);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return PointIsValid(OriginPoint) && PointIsValid(DestinationPoint);
    }

    private static bool TextObjectIsValid(Text textObject)
    {
        return textObject != null && !textObject.text.Equals(string.Empty);
    }

    private bool PointIsValid(Coordinate point)
    {
        int max = GridController.DimensionMax;
        int min = -max;
        return point.X <= max
            && point.X >= min
            && point.Y <= max
            && point.Y >= min
            && point.Z <= max
            && point.Z >= min;
    }

    private void UpdateGridController()
    {
        if (GridController == null)
        {
            GridController = FindObjectOfType(typeof(GridControl)) as GridControl;
        }
    }

    private void UpdateDestinationOnGrid()
    {
        if (PointIsValid(DestinationPoint))
        {
            GridController.CreateDestinationPoint(DestinationPoint.X, DestinationPoint.Y, DestinationPoint.Z);
        }
    }

    private float elapsedTime = 0.0f;
    void Update()
    {
        if (running)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > IterationPeriod)
            {
                elapsedTime = 0.0f;
                RunIteration();
                DisplayIteration();
            }
        }
    }

    private void RunIteration()
    {
        PathFinder.Update();
    }

    private void DisplayIteration()
    {
        if (PathFinder.Generation != lastGeneration)
        {
            lastGeneration = PathFinder.Generation;
            GridController.ResetLines();
        }
        else
        {
            Coordinate endpoint = PathFinder.GetFirstGenome().Path.Last();
            if (PointIsValid(endpoint))
            {
                GridController.CreateGridLine(LastLinePoint.X, LastLinePoint.Y, LastLinePoint.Z, endpoint.X, endpoint.Y, endpoint.Z);
                LastLinePoint = endpoint;

                DestinationPoint = PathFinder.DestinationPosition;
                UpdateDestinationOnGrid();
            }
        }
    }
}