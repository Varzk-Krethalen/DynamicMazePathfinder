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
    private float IterationPeriod { get; set; }
    private float MutationStrength { get; set; }
    private Coordinate LastLinePoint { get; set; }
    private Coordinate OriginPoint { get; set; }
    private Coordinate DestinationPoint { get; set; }

    private const string inputStringFindError = "Invalid Settings";
    private bool running = false;
    private int lastGeneration;

    public void RunPathfinder()
    {
        UpdateGridController();

        if (!running)
        {
            try
            {
                UpdateSettings();
                lastGeneration = 0;
                PathFinder = new GeneticAlgorithm(NumberOfGenomes, IterationsPerGeneration, OriginPoint, DestinationPoint, MutationStrength, CrossOver.CrossOverType.ONE_POINT);
                UpdateDestinationOnGrid();
                LastLinePoint = OriginPoint;
                SetOutput("Status", "Running");
            }
            catch (FormatException)
            {
                SetOutput("Status", inputStringFindError);
            }
            catch (Exception e)
            {
                SetOutput("Status", e.Message);
            }
        }
        else
        {
            SetOutput("Status", "Stopped");
        }
        running = !running;
    }

    private static void SetOutput(string tag, string statusString)
    {
        GameObject.FindWithTag("Status").GetComponent<Text>().text = statusString;
    }

    private void UpdateSettings() //TODO: get from field, TODO: alter mutation rate/crossover percent
    {
        OriginPoint = new Coordinate(0, 0, 0);

        try
        {
            int destX = int.Parse(GetInputStringByTag("DestX"));
            int destY = int.Parse(GetInputStringByTag("DestY"));
            int destZ = int.Parse(GetInputStringByTag("DestZ"));
            DestinationPoint = new Coordinate(destX, destY, destZ);
        }
        catch
        {
            System.Random rand = new System.Random();
            DestinationPoint = new Coordinate(rand.Next(1, GridController.DimensionMax), rand.Next(1, GridController.DimensionMax), rand.Next(1, GridController.DimensionMax));
        }

        try
        {
            IterationPeriod = float.Parse(GetInputStringByTag("IterationLength"));
        }
        catch
        {
            IterationPeriod = 0.5f;
        }

        try
        {
            IterationsPerGeneration = int.Parse(GetInputStringByTag("Iterations"));
        }
        catch
        {
            IterationsPerGeneration = (int)((Math.Abs(DestinationPoint.X) + Math.Abs(DestinationPoint.Y) + Math.Abs(DestinationPoint.Z)) * 1.2);
        }

        try
        {
            NumberOfGenomes = int.Parse(GetInputStringByTag("Genomes"));
        }
        catch
        {
            NumberOfGenomes = 20;
        }

        try
        {
            MutationStrength = float.Parse(GetInputStringByTag("Mutation"));
        }
        catch
        {
            MutationStrength = 1;
        }


        if (!(PointIsValid(OriginPoint) && PointIsValid(DestinationPoint)))
        {
            throw new Exception(inputStringFindError);
        }
    }

    private string GetInputStringByTag(string objectTag)
    {
        var textObject = GameObject.FindWithTag(objectTag).GetComponent<InputField>().textComponent;
        if (textObject != null && !textObject.text.Equals(string.Empty))
        {
            return textObject.text;

        }
        else
        {
            throw new Exception(inputStringFindError);
        }
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
            GridController.UpdateDestinationPoint(DestinationPoint.X, DestinationPoint.Y, DestinationPoint.Z);
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
            SetOutput("Fitness", $"Current Best Fitness: {PathFinder.GetFitness(PathFinder.GetFirstGenome())}");
            SetOutput("Status", $"Generation: {PathFinder.Generation}");
            lastGeneration = PathFinder.Generation;
            LastLinePoint = OriginPoint;
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