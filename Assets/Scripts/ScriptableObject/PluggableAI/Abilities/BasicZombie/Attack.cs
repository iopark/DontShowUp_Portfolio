using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(fileName = "Skill_Attack_", menuName = "PluggableSkill/Skill/Attack")]
public class Attack : SkillProperty
{
    #region GetSet Region for the Attacking Ability
    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField] private float attackAngle;
    public float AttackAngle { get { return attackAngle; } }
    [SerializeField] private float attackInterval;
    public float AttackInterval { get { return attackInterval; } }
    [SerializeField] private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }
    [SerializeField] private LayerMask targetMask;
    public LayerMask TargetMask { get { return targetMask; } }
    public EnemyAttacker Attacker { get; set; }
    #endregion
    public override void Perform()
    {

    }

    public Vector3? StartStrike()
    {
        //Assumption is that unit is already looking at the enemy. ?
        // no. assumption is that enemy is / was in the attacking range. 
        Collider[] colliders = Physics.OverlapSphere(Attacker.transform.position, attackRange, targetMask);
        if (colliders.Length == 0)
            return null;
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - Attacker.transform.position).normalized;

            if (Vector3.Dot(Attacker.transform.forward, dirTarget) < Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad))
                return null;
            return dirTarget;
        }
        return null;
    }
    public void Strike(Vector3 attackDir)
    {
        if (Physics.SphereCast(Attacker.transform.position, attackRange, attackDir, out RaycastHit hit, attackRange, targetMask))
        {
            IHittable target = hit.collider.GetComponent<IHittable>();
            target?.TakeHit(attackDamage);
        }
    }
    public void Strike()
    {
        if (Physics.SphereCast(Attacker.transform.position, attackRange, Attacker.transform.forward, out RaycastHit hit, attackRange, targetMask))
        {
            IHittable target = hit.collider.GetComponent<IHittable>();
            target?.TakeHit(attackDamage);
        }
    }
}
