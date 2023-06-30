using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPathHelper
{
    
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
        float threshHold = 3 * Vector3.SqrMagnitude(destinationPoint - startingPoint);
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