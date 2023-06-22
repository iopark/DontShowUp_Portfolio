using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Action_IdleWander_", menuName = "PluggableAI/Actions/Wander")]
public class IdleWanderAction : Action
{
    [SerializeField] private Act defaultMove;
    [SerializeField] private Act defaultRotate;
    [SerializeField] private Act defaultWander;
    public override void Act(StateController controller)
    {
        if (controller.FixToDir != Vector3.zero || controller.FixToDir != null)
            return; //FixToDir�� ���������� ������ ����Ʈ�� ���ؼ� �̵��� �ǽ��Ѵ�. 
        defaultMove.Perform(controller);
        if (controller.CurrentLookDir == Vector3.zero)
            defaultWander.Perform(controller);
        if (Vector3.Dot(controller.transform.forward, controller.CurrentLookDir) < 0.95f)
        {
            defaultRotate.Perform(controller);
            return;
        }
    }
}
