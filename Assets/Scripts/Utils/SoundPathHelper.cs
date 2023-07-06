using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPathHelper
{
    
}

public struct SoundPoint : IComparable<SoundPoint>
{
    public Cell cell;
    public float distance; 
    public SoundPoint (Cell cell, float distance)
    {
        this.cell = cell;
        this.distance = distance;
    }

    public int CompareTo(SoundPoint other)
    {
        int compare = distance.CompareTo(other.distance);
        if (compare == 0)
        {
            // if hCost of this is smaller than the comparing one, 
            // this would result in -1. // The result needs to be -1, if priority is higher 
            compare = cell.gCost.CompareTo(cell.gCost);
        }
        return compare;
    }
}

public class ValidSoundEvaluator
{
    public Vector2 startingPoint;
    public Vector2 destinationPoint;
    public Vector3[] soundPath;

    public float distanceThreshhold;
    //TODO: Time distance by the *2, and if the accumulated distance is less or equal to the threshhold, return true,
}

public struct SoundValidityCheckSlip
{
    public Vector3 startingPoint;
    public Vector3 destinationPoint;
    public Vector3[] soundPath;
    public bool isValid; 

    public SoundValidityCheckSlip (Vector3 startPoint, Vector3 destination, Vector3[] soundPath)
    {
        this.startingPoint = startPoint;
        this.destinationPoint = destination;
        this.soundPath = soundPath;
        isValid = true; // Default value for the validity testing 
        isValid = WallDoesNotInterfere(); 
    }

    private bool WallDoesNotInterfere()
    {
        Vector3 prev = startingPoint; 
        float threshHold = 2 * Vector3.SqrMagnitude(destinationPoint - startingPoint);
        float accumDist = 0f;  
        foreach (Vector3 point in soundPath)
        {
            //Calculate distance between each points. 
            float distance = Vector3.SqrMagnitude(point - prev); 
            accumDist += distance;
            prev = point;
        }
        return (accumDist <= threshHold);
    }
}