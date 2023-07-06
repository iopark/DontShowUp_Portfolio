using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_TraceState_", menuName = "PluggableAI/EnterAct/TraceState")]
public class EnterTraceState : Act
{
    [SerializeField] Action retracePath;
    [SerializeField] Act defaultRotate;
    [SerializeField] Act defaultMove;
    [SerializeField] float pointOffSet; 
    public override void Perform(StateController controller)
    {
        InitializeTraceState(controller); 
    }

    private void InitializeTraceState(StateController controller)
    {
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert);
        controller.RestartCoroutine(retracePath.GetType().Name, FollowSound(controller)); 
    }

    IEnumerator FollowSound(StateController controller)
    {
        if (controller.EnemyMover.TraceSoundPoints.Length <= 0)
            yield break;
        //controller.EnemyMover.TraceSoundPoints = traceablePath;
        int trackingIndex = 0;
        Vector3 currentWaypoint = controller.EnemyMover.TraceSoundPoints[0];
        controller.Sight.SetLookDirToPos(currentWaypoint);
        while (true)
        {
            defaultRotate.Perform(controller);
            if (Vector3.SqrMagnitude(currentWaypoint - controller.transform.position) < pointOffSet)
            {
                trackingIndex++;
                if (trackingIndex >= controller.EnemyMover.TraceSoundPoints.Length)
                {
                    controller.Auditory.HaveHeard = false;
                    yield break;
                }
                currentWaypoint = controller.EnemyMover.TraceSoundPoints[trackingIndex];
            }
            defaultMove.Perform(controller);
            yield return null;
        }
    }
}
