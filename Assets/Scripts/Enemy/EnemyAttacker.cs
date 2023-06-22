using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    #region 꼭 필요한 구성요소 
    Enemy Enemy { get; set; }
    public Attack DefaultAttack
    {
        get; set;
    }
    [SerializeField] private Attack defaultAttack;
    WaitForSeconds attackInterval;
    Vector3 attackDir; 
    public Vector3 AttackDir { get { return attackDir; } set{ attackDir = value; } }
    #endregion
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        DefaultAttack = GameManager.Resource.Instantiate(defaultAttack);
        attackInterval = new WaitForSeconds(DefaultAttack.AttackInterval);
        DefaultAttack.Attacker = this;
    }

    private void TryStrike()
    {
        DefaultAttack.Strike();
    }
    IEnumerator DoAttack()
    {
        Enemy.anim.SetTrigger(DefaultAttack.AnimTrigger);
        yield break; 
    }
}
