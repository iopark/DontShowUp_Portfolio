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
        get { return defaultAttack; }
    }
    [SerializeField] private Attack defaultAttack;
    WaitForSeconds attackInterval;
    [SerializeField] bool isAttacking; 
    public bool IsAttacking {  get { return isAttacking; } set { isAttacking = value; } }
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
    public void StopAttack()
    {
        isAttacking = false;
    }
    public void StrikePlayer()
    {
        DefaultAttack.Strike(); 
    }
    public void TryStrike()
    {
        if (isAttacking)
            return;
        isAttacking = true; // isAttacking is set to finished by animation or when player is out of sight. 
        enemyMover.CurrentSpeed = 0f;
        Enemy.anim.SetTrigger(defaultAttack.AnimTrigger);
        //attackRoutine = StartCoroutine(DoAttack());
    }
    //IEnumerator DoAttack()
    //{
    //    isAttacking = true;
    //    enemyMover.CurrentSpeed = 0f;
    //    Enemy.anim.SetTrigger(defaultAttack.AnimTrigger);
    //}
}