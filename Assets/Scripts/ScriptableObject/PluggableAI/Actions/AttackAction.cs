using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack_", menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    [SerializeField] Act defaultRotate;
    [SerializeField] float dotThreshHold;

    public override string actionName => typeof(AttackAction).Name; 

    public override void Act(StateController controller)
    {
        PerformAttack(controller);
    }

    private void PerformAttack(StateController controller)
    {
        //better to do so in the coroutine?
        //controller.EnemyMover.CurrentSpeed = 0f; 
        controller.RunAndSaveForReset(actionName, FaceTarget(controller)); 
    }

    IEnumerator FaceTarget(StateController controller)
    {
        while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
        {
            Debug.Log("FaceTarget"); 
            defaultRotate.Perform(controller);
            yield return null;
        }
        controller.EnemyAttacker.TryStrike();
        //Since coroutine is finished, stop the coroutine, and remove from the list 
        //controller.ResetCoroutine(coroutineKey);
    }
}
