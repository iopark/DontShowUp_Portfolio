using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    
    public static AnimRequestSlip MakeSlip (AnimType animType, string animTrigger, bool animBool, float? animFloat)
    {
        if (animBool == null && animFloat == null)
        {
            AnimRequestSlip newSlip = new AnimRequestSlip(animType, animTrigger);
            return newSlip; 
        }
        else if (animBool == null)
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
