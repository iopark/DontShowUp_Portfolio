using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(fileName = "Action_Aligning_", menuName = "PluggableAI/Actions/BasisAlignment")]
public class AlignOrientationAction : Action
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float dotThreshHold; 

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
                //controller.Sight.SetDirToLook(targetDir);
            }
            else
            {
                controller.EnemyMover.LookDir = targetDir;
                //controller.Sight.SetDirToLook(-targetDir);
            }
            controller.RequestMove(MoveType.RotateOnly, targetDir, RotatorMechanism(controller));
        }
    }

    IEnumerator RotatorMechanism(StateController controller)
    {
        Quaternion rotation = Quaternion.LookRotation(controller.EnemyMover.LookDir);
        while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
        {
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, 0.3f);
            yield return null;
        }
        //TODO: if the action is to fail? 
        controller.FinishedAction(true);
    }
}
