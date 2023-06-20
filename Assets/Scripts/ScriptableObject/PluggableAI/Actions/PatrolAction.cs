using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    [SerializeField] private float patrolSpeed;
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        if (!controller.patrolStatus)
        {
            controller.patrolStatus = true;
            Vector3 destination = controller.returnPoints[controller.patrolIndex];
            controller.transform.LookAt(destination);
            controller.characterController.Move(destination * patrolSpeed * Time.deltaTime);
        }

    }
}
