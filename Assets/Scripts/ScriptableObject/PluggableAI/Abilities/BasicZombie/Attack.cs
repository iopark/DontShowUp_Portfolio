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
    //[SerializeField] private float attackInterval;
    //public float AttackInterval { get { return attackInterval; } }
    [SerializeField] private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }
    [SerializeField] private LayerMask targetMask;
    public LayerMask TargetMask { get { return targetMask; } }
    public EnemyAttacker Attacker { get; set; }
    #endregion
    public override void Perform()
    {

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
        Vector3 attackDir = Attacker.transform.forward;
        attackDir.y = 0f;
        Debug.DrawRay(Attacker.transform.position, attackDir, Color.red); 
        if (Physics.SphereCast(Attacker.transform.position, Attacker.transform.lossyScale.x/2 , Attacker.transform.forward, out RaycastHit hit, attackRange, targetMask))
        {
            IHittable target = hit.collider.GetComponent<IHittable>();
            target?.TakeHit(attackDamage);
            Debug.Log(attackDamage); 
        }
    }
}
