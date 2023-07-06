using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_#_Pursuit", menuName = "PluggableAI/Decisions/PursuitDecision")]
public class PursuitDecision : Decision
{
    // Look Decision 과 다른점이 없다, 
    // Pursuit 은 ScanDecision 에 의거해서 진행해준다. 
    // Pursuit를 계속할지 않할지에 대해서만 결정을 내려준다. 
    [SerializeField] float resetTimer; 
    public override bool Decide(StateController controller)
    {
        return Pursuiting(controller);
    }

    private bool Pursuiting(StateController controller)
    {
        // As this is being tracked by the AlertstateDecision maker 
        if (controller.Sight.PlayerLocked == null)
        {
            return false;
        }

        if (controller.Sight.PlayerInSight == null)
        {
            controller.RunAndSaveForReset(nameof(CountdownPlayerMissingTime), CountdownPlayerMissingTime(controller)); 
        }
        else 
            controller.SignalCoroutineFinish(nameof(CountdownPlayerMissingTime));
        return true; 
    }

    IEnumerator CountdownPlayerMissingTime(StateController controller)
    {
        float time = 0f;
        while (time < resetTimer)
        {
            time += Time.deltaTime;
            yield return null; 
        }
        controller.Sight.PlayerLocked = null; 
    }
}
