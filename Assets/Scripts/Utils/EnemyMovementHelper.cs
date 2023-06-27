using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class EnemyMovementHelper
{
    public static void ReversePatrolPoints(this StateController controller)
    {
        Debug.Log("Reversed"); 
        controller.EnemyMover.PatrolPoints.Reverse();
        foreach (PatrolPoint p in controller.EnemyMover.PatrolPoints)
        {
            p.Reverse();
        }
    }

    public static void ResetPoints(this StateController controller)
    {
        controller.EnemyMover.PatrolPoints.Clear();
    }
}

public struct PatrolPoint
{
    public Vector3 Direction;
    public Vector3 worldPosition;

    public PatrolPoint(Vector3 direction, Vector3 worldPosition)
    {
        this.worldPosition = worldPosition;
        this.Direction = direction;
    }

    public void Reverse()
    {
        this.Direction = Direction * -1;
    }
}
public struct ActionRequestSlip
{
    public UnityEngine.Object bodyComponent; 
    public float interval;
    public Action<bool> callback;

    public ActionRequestSlip(UnityEngine.Object _bodyComponent, float interval, Action<bool> _callback)
    {
        this.bodyComponent = _bodyComponent;
        this.interval = interval;
        this.callback = _callback;
    }
}
public enum AnimType
{
    Trigger,
    Float,
    Bool,
    Size
}

public struct AnimRequestSlip
{
    public AnimType AnimType;
    public float? animFloat;
#nullable enable
    public string? animName;
    public bool? animBool; 

    //Trigger; 
    public AnimRequestSlip (AnimType animType, string animTrigger)
    {
        this.AnimType = animType;
        this.animName = animTrigger;
        this.animBool = null;
        this.animFloat = null;
    }
    //Float
    public AnimRequestSlip(AnimType animType, string animTrigger, float animFloat)
    {
        this.AnimType = animType;
        this.animName = animTrigger;
        this.animBool = null;
        this.animFloat = animFloat;
    }
    //Bool 
    public AnimRequestSlip(AnimType animType, string animTrigger, bool animBool)
    {
        this.AnimType = animType;
        this.animName = animTrigger;
        this.animBool = animBool;
        this.animFloat = null;
    }
    
    public static AnimRequestSlip MakeSlip (AnimType animType, string animTrigger, bool? animBool, float? animFloat)
    {
        if (animBool == null && animFloat == null)
        {
            AnimRequestSlip newSlip = new AnimRequestSlip(animType, animTrigger);
            return newSlip; 
        }
        else if (animFloat != null)
        {
            AnimRequestSlip newSlip = new AnimRequestSlip(animType, animTrigger, (float)animFloat);
            return newSlip;
        }
        else
        {
            AnimRequestSlip newSlip = new AnimRequestSlip(animType, animTrigger, (bool)animBool);
            return newSlip;
        }
    }
}

/// <summary>
/// Probably it is best for state controller to controll these. 
/// 
/// </summary>
/// 

public enum MoveType
{
    RotateOnly, 
    Move, 
    Chase
}

/// <summary>
/// Based off Coroutines? 
/// </summary>
public struct MoveRequestSlip : IEquatable<MoveRequestSlip>
{
    public const float newDirectionThreshhold = 0.96f;
    public const int newLocationThreshhold = 1; 
    public MoveType moveType; 
    //public Vector3 RequestedLocation;
    public Vector3 requestedDestination;
    public IEnumerator enumerator; 
    //public Action<bool> _callBack; //as should be defined by the statecontroller. 

    //public MoveRequestSlip(Vector3 requestDir, Vector3 RequestedLoc, IEnumerator enumerator)
    //{
    //    this.moveType = MoveType.Move;
    //    this.RequestedLocation = requestDir;
    //    this.RequestedDestination = RequestedLoc;
    //    this.enumerator = enumerator;
    //}

    //public MoveRequestSlip(Vector3 requestDir, IEnumerator enumerator)
    //{
    //    this.moveType = MoveType.RotateOnly; 
    //    this.RequestedLocation = requestDir;
    //    this.RequestedDestination = Vector3.zero;
    //    this.enumerator = enumerator; 
    //}

    public MoveRequestSlip(MoveType moveType, Vector3 requestedDestination, IEnumerator enumerator)
    {
        this.moveType= moveType;
        this.requestedDestination = requestedDestination;
        this.enumerator= enumerator;
    }

    /// <summary>
    /// if true, reject the slip since the requested location is nearby the previous location 
    /// if false, accept  it. 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(MoveRequestSlip other)
    {
        //only take request for new  destination where its larger than the 1f magnitude in value. 
        if (other.moveType == MoveType.RotateOnly)
        {
            return (Vector3.Dot(this.requestedDestination, other.requestedDestination) > newDirectionThreshhold); 
        }
        else if (other.moveType == MoveType.Move) 
        {
            //if distance between newly requested location is less than 1, it is treated as equal. 
            Vector3 offset = other.requestedDestination - this.requestedDestination;
            return Vector3.SqrMagnitude(offset) < newLocationThreshhold;
        }
        else
        {
            return false;
            //TODO: Chase Method 
        }

    }
}