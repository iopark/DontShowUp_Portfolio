using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyMovementHelper
{
    public static void ReversePatrolPoints(this StateController controller)
    {
        controller.EnemyMover.PatrolPoints.Reverse();
        foreach (PatrolPoint p in controller.EnemyMover.PatrolPoints)
        {
            p.Reverse();
        }
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
