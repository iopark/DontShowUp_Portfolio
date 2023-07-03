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
        defaultAttack = GameManager.Resource.Instantiate(defaultAttack, "Data/Zombie/FSM/Act/Act_Attack_BasicZombie");
        attackInterval = new WaitForSeconds(defaultAttack.AttackInterval);
        defaultAttack.Attacker = this;
    }
    public void FinishedAttacking()
    {
        isAttacking = false; 
    }

    public void StrikePlayer()
    {
        DefaultAttack.Strike(); 
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
            Enemy.anim.SetTrigger(defaultAttack.AnimTrigger);
            yield return attackInterval; 
        }
    }
}
