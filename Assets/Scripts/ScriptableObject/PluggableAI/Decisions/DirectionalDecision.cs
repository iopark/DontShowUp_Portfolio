using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Directional_", menuName = "PluggableAI/Decisions/Directional")]
public class DirectionalDecision : Decision
{
    //TODO: Kinda similar to the Align Actions. 
    public override bool Decide(StateController controller)
    {
        return true; 
    }


}
