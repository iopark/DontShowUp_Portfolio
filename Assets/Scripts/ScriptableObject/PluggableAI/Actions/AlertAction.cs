using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Alert_", menuName = "PluggableAI/Actions/Alert")]
public class AlertAction : Action
{
    //Basic Alert Action: Rotate and Scan in 360 degrees. 
    [SerializeField] float alertDuration;
    public override void Act(StateController controller)
    {
        controller.EnemyMover.Rotator(-controller.ForwardVector); 
    }
}
