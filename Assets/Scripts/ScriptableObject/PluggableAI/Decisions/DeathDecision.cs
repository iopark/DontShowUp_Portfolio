using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Death_", menuName = "PluggableAI/Decisions/Death")]
public class DeathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.Enemy.Health <= 0; 
    }
}
