using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DynamicPathfinder;
using System;

//[InitializeOnLoad]
public class PathfinderDisplayScript : MonoBehaviour
{
    public Button runButton;
    private GeneticAlgorithm PathFinder { get; set; }
    private GridControl GridController { get; set; }
    public int NumberOfGenomes { get; set; } = 20;
    public int IterationsPerGeneration { get; set; } = 20;
    public int DestinationX { get; set; } = 5;
    public int DestinationY { get; set; } = 5;
    public int DestinationZ { get; set; } = 5;

    private float iterationPeriod;
    //private static DynamicPathfinder.get last path?
    private bool running;

    PathfinderDisplayScript()
    {
        runButton.onClick.AddListener(() => RunPathfinder());
    }

    private void RunPathfinder()
    {
        if (!running)
        {
            if (GridController == null)
            {
                GetGridController();
            }
            UpdateSettings();
            PathFinder = new GeneticAlgorithm(NumberOfGenomes, IterationsPerGeneration, new Coordinate(0, 0, 0), new Coordinate(DestinationX, DestinationY, DestinationZ));
            GridController.CreateDestinationPoint(DestinationX, DestinationY, DestinationZ);
        }
        running = !running;
    }

    private void GetGridController()
    {
        GridController = FindObjectOfType(typeof(GridControl)) as GridControl;
    }

    private void UpdateSettings()
    {
        //get iteration period, genomes, etc
        NumberOfGenomes = 0;
        IterationsPerGeneration = 0;
        DestinationX = 0;
        DestinationY = 0;
        DestinationZ = 0;
        throw new NotImplementedException();
    }

    private float elapsedTime = 0.0f;
    public float period = 1.1f;
    void Update()
    {
        if (running)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > period)
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
        int startX, startY, startZ, endX, endY, endZ;
        startX = startY = startZ = endX = endY = endZ = 0;
        GridController.CreateGridLine(startX, startY, startZ, endX, endY, endZ);
    }
}