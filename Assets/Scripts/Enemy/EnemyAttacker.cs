using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    #region 꼭 필요한 구성요소 
    Enemy Enemy { get; set; }
    EnemyMover enemyMover; 
    public Attack DefaultAttack
    {
        get; set;
    }
    [SerializeField] private Attack defaultAttack;
    WaitForSeconds attackInterval;
    [SerializeField] bool isAttacking; 
    public bool IsAttacking {  get { return isAttacking; } }
    Vector3 attackDir; 
    public Vector3 AttackDir { get { return attackDir; } set{ attackDir = value; } }
    Coroutine attackRoutine; 
    #endregion
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        enemyMover = GetComponent<EnemyMover>();
        DefaultAttack = Instantiate(defaultAttack);
        attackInterval = new WaitForSeconds(DefaultAttack.AttackInterval);
        DefaultAttack.Attacker = this;
    }

    public void StrikePlayer()
    {
        //Called by the Animator Event 
        DefaultAttack.Strike();
        enemyMover.CurrentSpeed = Mathf.Lerp(0, enemyMover.AlertMoveSpeed, 0.4f); 
    }

    public void StopAttack()
    {
        if (attackRoutine == null)
            return;
        StopCoroutine(attackRoutine); 
        isAttacking = false;
    }

    public void TryStrike()
    {
        Debug.Log("TryAttack");

        if (isAttacking)
            return;
        attackRoutine = StartCoroutine(DoAttack());
    }
    IEnumerator DoAttack()
    {
        while (true)
        {
            Debug.Log("Attack"); 
            enemyMover.CurrentSpeed = 0f; 
            isAttacking = true; 
            Enemy.anim.SetTrigger(DefaultAttack.AnimTrigger);
            yield return attackInterval; 
        }
    }
}
