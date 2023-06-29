using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Pursuit_", menuName = "PluggableAI/Actions/Pursuit")]
public class PursuitAction : Action
{
    [SerializeField] float dotThreshold = 0.98f;
    [SerializeField] float distanceThreshhold = 0.1f;

    [Header("RequiredActs")]
    [SerializeField] Act defaultMove;
    [SerializeField] Act defaultRotate;

    [TextArea]
    string SequencingActs;
    [SerializeField] Act postAct;

    public override string actionName => typeof(PursuitAction).Name;

    public override void Act(StateController controller)
    {
        Pursuit(controller);
    }

    private void Pursuit(StateController controller)
    {
        // 
        // if scanner has found the target, try to seek it out. 
        // since scanner will identify target to track and choose its direction, Vector3 target = controller.Sight.FindTarget();
        //if (controller.Sight.PlayerInSight == Vector3.zero)k
        //    return;

        if (controller.Sight.PlayerLocked == null)
            return; 
        controller.RunAndSaveForReset(actionName, ChaseTarget(controller)); 
    }
    //IEnumerator RotatorMechanism(StateController controller)
    //{
    //    Quaternion rotation = Quaternion.LookRotation(controller.EnemyMover.LookDir);
    //    while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
    //    {
    //        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, 0.3f);
    //        yield return null;
    //    }
    //    //TODO: if the action is to fail? 
    //    controller.FinishedAction(true);
    //}

    //IEnumerator MoveToDestination(Vector3 destination, StateController controller)
    //{
    //    float distanceToTarget = Vector3.SqrMagnitude(destination - controller.transform.position);
    //    while (distanceToTarget > 0.1f)
    //    {
    //        distanceToTarget = Vector3.SqrMagnitude(destination - controller.transform.position);
    //        Vector3 lookDir = destination - controller.transform.position;
    //        lookDir.y = 0f;
    //        lookDir.Normalize();
    //        controller.transform.rotation = Quaternion.LookRotation(lookDir);
    //        while (Vector3.Dot(controller.transform.forward, lookDir) < dotThreshHold)
    //        {
    //            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, controller.transform.rotation, 0.03f);
    //            yield return null;
    //        }
    //        controller.Enemy.characterController.Move(lookDir * controller.CurrentSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //    controller.FinishedAction(true, UpdatePatrolIndex);
    //}

    IEnumerator ChaseTarget(StateController controller)
    {
        float distanceToTarget;
        //Vector3 lookDir;
        //Quaternion rotation; 
        while (controller.Sight.PlayerLocked != null)
        {
            distanceToTarget = Vector3.SqrMagnitude(controller.Sight.PlayerLocked.position - controller.transform.position);
            //lookDir = controller.Sight.PlayerLocked.position - controller.transform.position;
            //lookDir.y = 0f; 
            //lookDir.Normalize();
            //controller.EnemyMover.LookDir = lookDir;
            controller.Sight.SetDirToPlayer();
            defaultRotate.Perform(controller);
            defaultMove.Perform(controller);
            Debug.Log($"PursuitAction{controller.EnemyMover.LookDir}");
            //while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshold)
            //{
            //    defaultRotate.Perform(controller);
            //    yield return null;
            //}
            //while (distanceToTarget > distanceThreshhold)
            //{
            //    defaultMove.Perform(controller);
            //    yield return null;
            //}
            yield return null;
        }
    }

}
