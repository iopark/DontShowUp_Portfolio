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
    public override void Perform(StateController controller)
    {
        controller.anim.SetTrigger(animTrigger);
        //Enemy needs to attack on a interval, 
        // case 1. Enemy should attack immediately upon target within the attack range 
        // case 2. Enemy should attack only after certain interval 
        // thus, create isAttacking bool variable in appropriate place, 
        // and if !isAttacking, execute attack coroutine
        // Attack Coroutine 
            Strike(controller); 
    }

    public void Strike(StateController controller)
    {
        controller.CurrentSpeed = 0f; 
        Collider[] colliders = Physics.OverlapSphere(controller.transform.position, attackRange, targetMask);
        if (colliders.Length == 0)
            return;
        foreach (Collider collider in colliders)
        {
            //2. �÷��̾� ���� �տ� �ִ����� ���ؼ� Ȯ���۾� �ʿ� 
            Vector3 dirTarget = (collider.transform.position - controller.transform.position).normalized;

            //3. �÷��̾� ���� ������ �� 
            if (Vector3.Dot(controller.transform.forward, dirTarget) < Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad))
                return;
            // ������ ������ ����: Dot product�� forward����, Target ������ Vector ���� ���ؼ� �����ϴµ�, 
            // �̴� Cos(angle) �� �����ϴ�. �̴� a.normal * b.normal �� ������ �����ϴ�. 
            // 
            IHittable target = collider.GetComponent<IHittable>();
            target?.TakeHit(attackDamage);
        }
    }
}
