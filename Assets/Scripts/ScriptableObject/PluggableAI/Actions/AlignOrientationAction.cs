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
        if (controller.FixToDir != Vector3.zero)
        {
            Turn(controller);
        }// if FixToDir is not Zero, its probably adjusting itself. 

        //if FixToDir is zero, intializeFixToDir. 
        Vector3 targetDir = Vector3.zero;
        Vector3[] searchPoint = controller.Sight.SightEdgesInDir(2);
        foreach (Vector3 dir in searchPoint)
        {
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, dir, out hit, controller.CurrentStat.sightDepth, wallLayer))
            {
                targetDir = Vector3.Cross(hit.collider.transform.up, hit.normal);
                break;
            }
        }
        if (targetDir != Vector3.zero)
        {
            if (Vector3.Dot(targetDir, controller.transform.forward) > 0)
            {
                controller.FixToDir = targetDir;
            }
            else
                controller.FixToDir = -targetDir;
        }
    }
    private void Turn(StateController controller)
    {
        Quaternion rotation = Quaternion.LookRotation(controller.FixToDir);
        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, 0.1f);
    }
}
