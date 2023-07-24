using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnterAct_PursuitPhase_", menuName = "PluggableAI/EnterAct/PursuitPhaseAct")]
public class EnterPursuitState : Act
{
    [SerializeField] Sound alertSound;
    public override void Perform(StateController controller)
    {
        GameManager.AudioManager.PlayEffect(alertSound);
    }
}
