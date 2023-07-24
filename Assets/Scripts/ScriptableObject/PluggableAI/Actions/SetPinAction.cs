using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_SetPin_", menuName = "PluggableAI/Actions/SetPin")]
public class SetPinAction : Action
{
    [SerializeField] private float setPinInterval;
    [SerializeField] private Act defaultRotate; 
    [SerializeField] private Act defaultMove; //Move

    public override string actionName => typeof(SetPinAction).Name;

    public override void Act(StateController controller)
    {
        SetPin(controller);
    }
    /// <summary>
    /// The Advanted SetPin should be able to scan map around, find potential spot to scan before searching 
    /// </summary>
    /// <param name="controller"></param>
    protected virtual void SetPin(StateController controller)
    {
        defaultRotate.Perform(controller); 
        defaultMove.Perform(controller);
        if (controller.EnemyMover.CheckElapsedTime(setPinInterval))
        {
            PatrolPoint pin = new PatrolPoint(controller.EnemyMover.LookDir, controller.EnemyMover.transform.position);
            controller.EnemyMover.PatrolPoints.Add(pin);
        }
    }
}
