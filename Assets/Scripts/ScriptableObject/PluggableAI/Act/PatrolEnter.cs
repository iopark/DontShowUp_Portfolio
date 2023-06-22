using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Act_PrePatrol_", menuName = "PluggableAI/Act/PrePatrol")]
public class PatrolEnter : Act
{
    public override void Perform(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    public void SetForSearch()
    {
        //1. Either return to the nearest patrol point, BasicZombieA 
        //2. or start searching BasicZombieB
    }
}
