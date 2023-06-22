using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_IdleEnd_", menuName = "PluggableAI/Decisions/IdleEnd")]
public class IdleEndDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        SpawningAction();
        return true;
    }

    private void SpawningAction()
    {
        //Different Enemy may add a behaviour 
    }
}
