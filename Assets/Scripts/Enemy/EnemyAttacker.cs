using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    #region 꼭 필요한 구성요소 
    Enemy Enemy { get; set; }
    Animator anim { get; set; }
    [SerializeField] private Attack defaultAttack;
    public Attack DefaultAttack
    {
        get; set;
    }

    WaitForSeconds attackInterval;
    bool isAttacking;
    Vector3 attackDir; 
    public Vector3 AttackDir { get { return attackDir; } set{ attackDir = value; } }
    #endregion
    private void Awake()
    {
        isAttacking = false; 
        anim = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        DefaultAttack = Instantiate(defaultAttack);
        attackInterval = new WaitForSeconds(DefaultAttack.AttackInterval);
        DefaultAttack.Attacker = this;
    }

    public void SetAttackAsDone()
    {
        isAttacking = false;
    }

    public void StrikePlayer()
    {
        //Called by the Animator Event 
        DefaultAttack.Strike();
    }

    public void TryStrike()
    {
        if (isAttacking)
            return;
        if (attackDir == Vector3.zero)
            return; 
        StartCoroutine(DoAttack());
    }
    IEnumerator DoAttack()
    {
        Debug.Log(attackDir); 
        // when should this coroutine stop? 
        // if attackDir is still present, 
        while (attackDir != Vector3.zero)
        {
            isAttacking = true; 
            Enemy.anim.SetTrigger(DefaultAttack.AnimTrigger);
            yield return attackInterval; 
        }
        isAttacking = false; 
        yield break;
    }
}
