using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DynamicPathfinder;

[InitializeOnLoad]
public class PathfinderDisplayScript : MonoBehaviour
{
    private static GeneticAlgorithm PathFinder { get; set; }
    static PathfinderDisplayScript()
    {
        PathFinder = new GeneticAlgorithm(20, 20, new Coordinate(0,0,0), new Coordinate(5,5,5));
        //Create grid, define start
        //Create the objects here!
        EditorApplication.update += Update;
    }

    //call a function for starting a new population?
    //should use it for resetting path & destination


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    static void Update() //add time limit mechanism? Else we get 60+ iterations a second
    {
        //run iteration, show selected genome's path on screen
        //if iteration > x, new generation
        //if generation > y, wait for input
    }
}
//create a dll for algorithm