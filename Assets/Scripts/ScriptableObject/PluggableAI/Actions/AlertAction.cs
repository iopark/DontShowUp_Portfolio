using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Alert_", menuName = "PluggableAI/Actions/Alert")]
public class AlertAction : Action
{
    [SerializeField] Act defaultWander; 
    [SerializeField] Act defaultMove; 
    public override void Act(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    private void StayAlert(StateController controller)
    {
        defaultMove.Perform(controller); 
    }

}
