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
        if (Physics.Raycast(controller.transform.position, controller.EnemyMover.LookDir, out hit, controller.Sight.Range, wallMask))
        {
            Vector3 newDir = controller.EnemyMover.LookDir - (2 * Vector3.Dot(hit.normal, controller.EnemyMover.LookDir) * hit.normal);
            controller.EnemyMover.LookDir = newDir;
        }
    }
}
