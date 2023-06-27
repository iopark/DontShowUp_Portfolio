using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Act_Rotate_", menuName = "PluggableAI/Act/Rotate")]
public class Rotate : Act
{
    public override void Perform(StateController controller)
    {
        Rotator(controller);
    }
    private void Rotator(StateController controller)
    {
        controller.EnemyMover.Rotator(); 
    }
}
