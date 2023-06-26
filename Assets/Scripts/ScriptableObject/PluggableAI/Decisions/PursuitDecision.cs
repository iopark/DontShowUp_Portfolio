using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_Pursuit_", menuName = "PluggableAI/Decisions/PursuitDecision")]
public class PursuitDecision : Decision
{
    // Look Decision �� �ٸ����� ����, 
    // Pursuit �� ScanDecision �� �ǰ��ؼ� �������ش�. 
    // Pursuit�� ������� �������� ���ؼ��� ������ �����ش�. 
    [SerializeField] float resetScanner;
    public override bool Decide(StateController controller)
    {
        return Pursuiting(controller);
    }
    /// <summary>
    /// return true if Player is still in Sgith, else return false. 
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    private bool Pursuiting(StateController controller)
    {

        return controller.Sight.LockInTarget == null && controller.Sight.CheckElapsedTime(resetScanner); 
            //controller.Sight.PlayerInSight != Vector3.zero;
    }
}
