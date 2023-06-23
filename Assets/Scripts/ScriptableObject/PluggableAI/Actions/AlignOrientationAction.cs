using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Aligning_", menuName = "PluggableAI/Actions/BasisAlignment")]
public class AlignOrientationAction : Action
{
    [SerializeField] private LayerMask wallLayer;

    public override void Act(StateController controller)
    {
        Align(controller);
    }

    private void Align(StateController controller)
    {
        if (controller.EnemyMover.AlignDir != Vector3.zero)
        {
            controller.EnemyMover.Rotator(controller.EnemyMover.AlignDir);
            return;
        } 
        Vector3 targetDir = Vector3.zero;
        Vector3[] searchPoint = controller.Sight.SightEdgesInDir(2);
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
                controller.EnemyMover.AlignDir = targetDir;
                //controller.Sight.SetDirToLook(targetDir);
            }
            else
            {
                controller.EnemyMover.AlignDir = targetDir;
                //controller.Sight.SetDirToLook(-targetDir);
            }
        }
    }
}
