using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsWithDefault : Action
{
    public Act defaultAct;

    public override string actionName => throw new System.NotImplementedException();

    public override void Act(StateController controller)
    {

    }
}
