using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Act_Attack_", menuName ="PluggableAI/Act/Attack")]
public class Attack : Act
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackAngle; 
    [SerializeField] private float attackInterval;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private string animTrigger; 
    public override void Perform(StateController controller)
    {
        controller.anim.SetTrigger(animTrigger);
        Strike(controller); 
    }

    public void Strike(StateController controller)
    {
        Collider[] colliders = Physics.OverlapSphere(controller.transform.position, attackRange, targetMask);
        if (colliders.Length == 0)
            return;
        foreach (Collider collider in colliders)
        {
            //2. 플레이어 기준 앞에 있는지에 대해서 확인작업 필요 
            Vector3 dirTarget = (collider.transform.position - controller.transform.position).normalized;

            //3. 플레이어 지정 각도와 비교 
            if (Vector3.Dot(controller.transform.forward, dirTarget) < Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad))
                return;
            // 반으로 나누는 이유: Dot product를 forward값과, Target 까지의 Vector 값에 대해서 검출하는데, 
            // 이는 Cos(angle) 과 동일하다. 이는 a.normal * b.normal 인 이유와 동일하다. 
            // 
            IHittable target = collider.GetComponent<IHittable>();
            target?.TakeHit(attackDamage);
        }
    }
}
