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

    IEnumerator ChaseTarget(StateController controller)
    {
        float distanceToTarget;
        //Vector3 lookDir;
        //Quaternion rotation; 
        while (controller.Sight.PlayerLocked != null)
        {
            distanceToTarget = Vector3.SqrMagnitude(controller.Sight.PlayerLocked.position - controller.transform.position);
            controller.Sight.SetDirToPlayer();
            defaultRotate.Perform(controller);
            defaultMove.Perform(controller);
            yield return null;
        }
        controller.ResetCoroutine(actionName); 
    }

}
