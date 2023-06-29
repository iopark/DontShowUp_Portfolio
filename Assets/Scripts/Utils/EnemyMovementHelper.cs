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
        Vector3 prevPoint = Vector3.zero; 
        Debug.Log("Reversed"); 
        controller.EnemyMover.PatrolPoints.Reverse();
        for (int i = 0; i < controller.EnemyMover.PatrolPoints.Count; i++)
        {
            if (i == 0)
            {
                controller.EnemyMover.PatrolPoints[0].Reverse();
                prevPoint = controller.EnemyMover.PatrolPoints[0].worldPosition;
                continue; 
            }
            controller.EnemyMover.PatrolPoints[i].Reverse(prevPoint);
            prevPoint = controller.EnemyMover.PatrolPoints[i].worldPosition; 
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

    public void Reverse(Vector3 pivotPoint)
    {
        Vector3 temp = pivotPoint - this.worldPosition;
        temp.y = 0f;
        temp.Normalize();
        this.Direction = temp; 
    }

    private void RenewPreviousPointDir(PatrolPoint[] savedList)
    {

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
public struct MoveRequestSlip
{
    public string name; 
    public IEnumerator enumerator;

    public MoveRequestSlip(string name,  IEnumerator enumerator)
    {
        this.name = name; 
        this.enumerator = enumerator;
    }
}

public struct CoroutineSlip : IEquatable<string>
{
    public string coroutineKey;
    public IEnumerator? routine; 

    public CoroutineSlip (string coroutineKey, IEnumerator routine)
    {
        this.coroutineKey = coroutineKey;
        this.routine = routine;
    }

    public void ChangeRoutine(IEnumerator routine)
    {
        this.routine = routine; 
    }

    public void SetToNull()
    {
        this.routine = null; 
    }
    public bool Equals(string requestKey)
    {
        return requestKey == coroutineKey;
    }
}