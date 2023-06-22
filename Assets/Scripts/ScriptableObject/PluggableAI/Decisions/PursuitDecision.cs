using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_#_Pursuit", menuName = "PluggableAI/Decisions/PursuitDecision")]
public class PursuitDecision : Decision
{
    // Look Decision �� �ٸ����� ����, 
    // Pursuit �� ScanDecision �� �ǰ��ؼ� �������ش�. 
    // Pursuit�� ������� �������� ���ؼ��� ������ �����ش�. 
    public override bool Decide(StateController controller)
    {
        return Pursuiting(controller);
    }

    private bool Pursuiting(StateController controller)
    {
        Vector3 target = controller.Sight.FindTarget();
        if (target == Vector3.zero)
        {
            return false;
        }
        return true;
    }
}
