using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : Act
{
    [SerializeField] private LayerMask wallMask; 
    public override void Perform(StateController controller)
    {
        CheckWall(controller);
    }

    private void CheckWall(StateController controller)
    {
        RaycastHit hit; 
        if (Physics.Raycast(controller.transform.position, controller.CurrentLookDir, out hit, controller.CurrentStat.sightDepth, wallMask))
        {
            Vector3 newDir = controller.CurrentLookDir - (2 * Vector3.Dot(hit.normal, controller.CurrentLookDir) * hit.normal); 
            controller.CurrentLookDir = newDir;
        }
    }
}
