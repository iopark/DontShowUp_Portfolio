using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(fileName = "Action_Aligning_", menuName = "PluggableAI/Actions/BasisAlignment")]
public class AlignOrientationAction : Action
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float dotThreshHold;
    [SerializeField] Act defaultRotate; 

    public override string actionName => typeof(AlignOrientationAction).Name; 

    public override void Act(StateController controller)
    {
        Align(controller);
        
    }

    private void Align(StateController controller)
    {
        Vector3 targetDir = Vector3.zero;
        Vector3[] searchPoint = controller.Sight.SightEdgesInDir(2);
        if (searchPoint.Length < 0)
            return;
        foreach (Vector3 dir in searchPoint)
        {
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, dir, out hit, controller.Sight.Range, wallLayer))
            {
                targetDir = Vector3.Cross(hit.collider.transform.up, hit.normal);
                break;
            }
        }
        if (targetDir != Vector3.zero)
        {
            if (Vector3.Dot(targetDir, controller.transform.forward) > 0)
            {
                controller.EnemyMover.LookDir = targetDir;
            }
            else
            {
                controller.EnemyMover.LookDir = targetDir;
            }
            controller.RunAndSaveForReset(actionName, RotatorMechanism(controller));
        }
    }

    IEnumerator RotatorMechanism(StateController controller)
    {
        while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
        {
            defaultRotate.Perform(controller); 
            yield return null;
        }
        controller.SignalCoroutineFinish(actionName); 
    }
}
