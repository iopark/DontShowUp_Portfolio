using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensory : MonoBehaviour
{
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
    #region testing Locking Target
    [SerializeField] private Transform playerLocked;
    private float sightThreshhold; 
    float SightThreshHold
    {
        set { sightThreshhold = value; }
    }
    public Transform PlayerLocked
    {
        get { return playerLocked; }
        set { playerLocked = value; }
    }
    Vector3 DirToLockedTarget
    {
        get
        {
            if (playerLocked == null)
                return Vector3.zero;
            Vector3 toLockedTarget = playerLocked.position - transform.position;
            toLockedTarget.y = transform.position.y;
            toLockedTarget.Normalize();
            return toLockedTarget;
        }
    }

    Vector3 distanceToTarget; 
    private float pinIntervalTimer;
    public float PinIntervalTimer { get { return pinIntervalTimer; } set { pinIntervalTimer = value; } }
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
    #endregion
    public float Range { get { return range; }
        set { range = value; } }

    public float Angle { get { return angle; }
        set { angle = value; } }

    private Vector3 LookDir { set { EnemyMover.LookDir = value; } }
    Vector3 tempDir; 
    #endregion
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        EnemyMover = GetComponent<EnemyMover>();
        EnemyAttacker = GetComponent<EnemyAttacker>();
    }
    private void Start()
    {
        sightThreshhold = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
        Enemy.CurrentStat.SyncSightData(this);
    }
    //TODO: Target must be continuing to search for the target, how can I implement this together with the FindTarget 
    public void SetLookDirToPos(Vector3 targetPos)
    {
        targetPos.y = 0f;
        tempDir = targetPos - transform.position;
        tempDir.Normalize();
        LookDir = tempDir;
    }

    public void SetDirToLook(Vector3 direction)
    {
        LookDir = direction;
    }

    public void SetDirToPlayer()
    {
        if (PlayerLocked == null)
        {
            Debug.Log("PlayerPosition is somewhat Zero"); 
            return;
        }
        tempDir = PlayerLocked.position - transform.position;
        tempDir.y = 0f;
        tempDir.Normalize();
        LookDir = tempDir; 
    }
    public bool FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        if (colliders.Length == 0)
        {
            PlayerInSight = Vector3.zero;
            return false;
        }
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = collider.transform.position - transform.position;
            dirTarget.y = 0f; 
            dirTarget.Normalize();

            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                PlayerInSight = Vector3.zero;
                continue;
            }

            Vector2 distToTarget = (Vector2)collider.transform.position - (Vector2)transform.position; 
            float distance = Vector2.SqrMagnitude(distToTarget);
            Debug.DrawRay(transform.position, dirTarget, Color.red); 
            if (Physics.Raycast(transform.position, dirTarget, distance, obstacleMask))
            {
                PlayerInSight = Vector3.zero;
                continue;
            }

            SetTarget(collider.transform); 
            return true;
        }
        return false;
    }



    private void SetTarget(Transform transform)
    {
        PinIntervalTimer = 0; // if target is found, set the PinIntervalTimer to 0 again. 
        playerInSight = transform.position;
        playerLocked = transform;
    }
    //Based on the Locked Target State 
    public bool AccessForAttackRange()
    {
        if (playerLocked == null)
            return false;
        //RaycastHit hit;
        //if (!Physics.Raycast(transform.position, DirToLockedTarget, out hit, EnemyAttacker.DefaultAttack.AttackRange, targetMask))
        //    return false;
        //SetDirToTargetForChase(hit.point);
        //EnemyAttacker.AttackDir = DirToLockedTarget; 
        distanceToTarget = playerLocked.transform.position - transform.position;
        if (Vector3.SqrMagnitude(distanceToTarget) > EnemyAttacker.DefaultAttack.AttackRange)
            return false;
        return true;
    }
    public bool AccessForAttack(float attackRange)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        if (colliders.Length == 0)
            return false; 
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirTarget) < sightThreshhold)
                continue;

            Vector3 distToTarget = dirTarget - transform.position;
            float distance = Vector3.SqrMagnitude(distToTarget);
            if (Physics.Raycast(transform.position, dirTarget, distance, obstacleMask))
                continue;
            Vector3 dir = (collider.transform.position - transform.position).normalized; 
            Debug.DrawRay(transform.position, dir, Color.red); 
            playerInSight = collider.transform.position;
            SetLookDirToPos(playerInSight);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true if player is at the blindspot. 
    /// </summary>
    /// <param name="playerDir"></param>
    /// <returns></returns>
    public bool IsPlayerAtBlindSpot(Vector3 playerDir)
    {
        return (Vector3.Dot(transform.forward, playerDir) < sightThreshhold * -1); 
    }
    public bool AccessForPursuit()
    {
        tempDir = PlayerLocked.transform.position - transform.position; 
        tempDir.y = transform.position.y;
        tempDir.Normalize();
        if (IsPlayerAtBlindSpot(tempDir))
            return false; 
        if (Physics.Raycast(transform.position, tempDir, out RaycastHit hit, Enemy.CurrentStat.maxDepth, targetMask))
        {
            PinIntervalTimer = 0;
            return true;
        }

        //if (hit.collider == null || hit.collider.tag != "Player")
        //    return false;

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
        //player �������� �����ϱ⿡, where player front is z axis, 
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
