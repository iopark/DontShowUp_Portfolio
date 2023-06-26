using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SightSensory : MonoBehaviour
{
    public enum STATE
    {
        Normal,
        Alert
    }
    #region GetSet Sight Sensory Properties 
    Enemy Enemy { get; set; }
    EnemyMover EnemyMover { get; set; }
    EnemyAttacker EnemyAttacker { get; set; }
    [SerializeField] bool debug;
    [SerializeField] float range;
    [SerializeField, Range(0, 360f)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    public LayerMask TargetMask { get { return targetMask; } set { targetMask = value; } }
    public LayerMask ObstacleMask { get { return obstacleMask; } set { obstacleMask = value; } }

    private Vector3 playerInSight;
    public Vector3 PlayerInSight { get { return playerInSight; } set { playerInSight = value; } }
    #region LockInTarget Testing 
    //Testing 
    [SerializeField] private GameObject lockInTarget; 
    public GameObject LockInTarget
    {
        get { return lockInTarget; }
        set { lockInTarget = value; }
    }
    private float pinIntervalTimer;
    public float PinIntervalTimer { get { return pinIntervalTimer; } set { pinIntervalTimer = value; } }
    #endregion
    public float Range { get { return range; }
        set { range = value; } }

    public float Angle { get { return angle; }
        set { angle = value; } }

    private Vector3 LookDir { set { EnemyMover.LookDir = value; } }
    #endregion
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        EnemyMover = GetComponent<EnemyMover>();
        EnemyAttacker = GetComponent<EnemyAttacker>();
    }
    private void Start()
    {
        Enemy.CurrentStat.SyncSightData(this);
    }
    //TODO: Target must be continuing to search for the target, how can I implement this together with the FindTarget 

    /// <summary>
    /// Will proceed to set LookDir for EnemyMover, a direction in which unit will pursue for actions. 
    /// </summary>
    /// <param name="targetPos"></param>
    public void SetDirToTargetForChase(Vector3 targetPos)
    {
        targetPos.y = transform.position.y;
        Vector3 lookDirection = (targetPos - transform.position).normalized;
        LookDir = lookDirection;
    }

    public void SetDirToLook(Vector3 direction)
    {
        LookDir = direction;
    }

    public void ChangeSightByState(STATE state)
    {
        switch (state)
        {
            case STATE.Normal:
                range = Enemy.CurrentStat.normalSightDepth; angle = Enemy.CurrentStat.normalSightAngle; break;
            case STATE.Alert:
                range = Enemy.CurrentStat.alertSightDepth; angle = Enemy.CurrentStat.alertSightAngle; break;
        }
    }
    public Vector3 FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        if (colliders.Length == 0)
            return Vector3.zero;
        foreach (Collider collider in colliders)
        {
            //2. 플레이어 기준 앞에 있는지에 대해서 확인작업 필요 
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            //3. 플레이어 지정 각도와 비교 
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            //4. 중간에 장애물이 없는지 

            Vector3 distToTarget = dirTarget - transform.position;
            float distance = Vector3.SqrMagnitude(distToTarget);
            if (Physics.Raycast(transform.position, dirTarget, distance, obstacleMask))
                continue;

            playerInSight = collider.transform.position;
            lockInTarget = collider.gameObject; 
            return playerInSight;
        }
        return Vector3.zero;
    }
    public bool CheckElapsedTime(float time)
    {
        PinIntervalTimer += Time.deltaTime;
        if (PinIntervalTimer >= time)
        {
            PinIntervalTimer = 0;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Determines whether player is in appropriate range for the Attack attempt (triggering attack simulation) 
    /// </summary>
    /// <param name="attackRange"></param>
    /// <returns></returns>
    public bool AccessForAttack(float attackRange)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        if (colliders.Length == 0)
        {
            EnemyAttacker.AttackDir = Vector3.zero;
            return false;
        }
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            //1. 플레이어 지정 각도와 비교 
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                EnemyAttacker.AttackDir = Vector3.zero;
                continue;
            }

            Debug.Log("FoundTarget");
            //2. 중간에 장애물이 없는지 
            Vector3 distToTarget = dirTarget - transform.position;
            float distance = Vector3.SqrMagnitude(distToTarget);
            if (Physics.Raycast(transform.position, dirTarget, distance, obstacleMask))
            {
                EnemyAttacker.AttackDir = Vector3.zero;
                continue;
            }
            Vector3 dir = (collider.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, dir, Color.red);
            Vector3 position = collider.transform.position;
            playerInSight = position;

            SetDirToTargetForChase(playerInSight);

            EnemyAttacker.AttackDir = EnemyMover.LookDir;
            return true;
        }
        EnemyAttacker.AttackDir = Vector3.zero;
        return false;
    }


    //private void Trace(Vector3 target)
    //{
    //    //This can transfer into TraceState 
    //    lookDir = (target - body.transform.position).normalized;
    //    lookDir.y = body.transform.position.y;
    //    body.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up); 
    //    body.Move(lookDir * speed *Time.deltaTime);
    //    anim.SetBool("Walk Forward", true); 
    //}

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        // where .eulerAngle.y returns rotation angle from the y-axis in a Space.World 
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        // 
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rightDir * range);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftDir * range);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
        //player 기준으로 생성하기에, where player front is z axis, 
    }

    public Vector3[] SightEdgesInDir(int interval)
    {
        Vector3[] dirs = new Vector3[interval];
        //float incrementSize = angle/interval;
        //for (int i = 0; i < interval; i++)
        //{
        //    float dirAngle = transform.eulerAngles.y - (angle * 0.5f) + i * incrementSize;

        //    dirs.Append(AngleToDir(dirAngle));
        //}
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        dirs[0] = rightDir;
        // where .eulerAngle.y returns rotation angle from the y-axis in a Space.World 
        //Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        dirs[1] = (AngleToDir(transform.eulerAngles.y - angle * 0.5f));

        return dirs;
    }
}

