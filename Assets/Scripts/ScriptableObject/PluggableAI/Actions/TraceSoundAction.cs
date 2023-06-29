using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceSoundAction : Action
{
    public override string actionName => typeof(TraceSoundAction).Name;
    [SerializeField] private Act defaultMove;
    [SerializeField] private Act defaultRotate;
    [SerializeField] private float pointOffSet;
    [SerializeField] private float dotThreshHold;

    public override void Act(StateController controller)
    {
        if (controller.EnemyMover.CurrentSpeed != controller.Enemy.CurrentStat.alertMoveSpeed)
            controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert);
    }

    public void TraceSound(StateController controller)
    {
        controller.RunAndSaveForReset(actionName, FollowSound(controller, controller.EnemyMover.TraceSoundPoints)); 
    }

    IEnumerator FollowSound(StateController controller, Vector3[] traceablePath)
    {
        controller.EnemyMover.TraceSoundPoints = traceablePath;
        int trackingIndex = 0;
        Vector3 currentWaypoint = traceablePath[0];
        controller.Sight.SetLookDirToPos(currentWaypoint);
        while (true)
        {
            defaultRotate.Perform(controller); 
            if (Vector3.SqrMagnitude(currentWaypoint - controller.transform.position) < pointOffSet)
            {
                trackingIndex++;
                if (trackingIndex >= traceablePath.Length)
                {
                    controller.Auditory.HaveHeard = false;
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }
            defaultMove.Perform(controller); 
            yield return null;
        }
    }
}
