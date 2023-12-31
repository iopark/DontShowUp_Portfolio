using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack_", menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    [SerializeField] Act defaultRotate;
    [SerializeField] float dotThreshHold;

    public override string actionName => nameof(AttackAction); 

    public override void Act(StateController controller)
    {
        PerformAttack(controller);
    }

    private void PerformAttack(StateController controller)
    {
        controller.RunAndSaveForReset(actionName, FaceTarget(controller));
        controller.EnemyAttacker.TryStrike();
    }

    IEnumerator FaceTarget(StateController controller)
    {
        //Debug.Log("Facingtarget"); 
        while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
        {
            defaultRotate.Perform(controller);
            yield return null; 
        }
        controller.ResetCoroutine(actionName);

        //Since coroutine is finished, stop the coroutine, and remove from the list 
        //controller.ResetCoroutine(coroutineKey);
    }
}
