using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyMover : MonoBehaviour
{
    [Header("Debugging Purposes")]
    public Vector3 currentDestination;
    TrailRenderer trail; 
    public bool debug; 

    CharacterController characterController;
    SoundSensory Auditory { get; set; }
    SightSensory Sight { get; set; }
    Animator animator;
    Enemy Enemy { get; set; }
    [SerializeField] Act defaultMove; //Scriptable Object to Instantiate and put to use. 
    public Act DefaultMove { get; private set; }
    #region Pertaining to Move 
    //Moving Abilities 
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set
        {
            animator.SetFloat("Speed", value);
            currentSpeed = value;
        }
    }
    private float normalMoveSpeed; 
    public float NormalMoveSpeed { get { return normalMoveSpeed; } set { normalMoveSpeed = value; } }
    private float alertMoveSpeed;

    public float AlertMoveSpeed
    {
        get { return alertMoveSpeed; }
        set { alertMoveSpeed = value; }
    }
    private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
    private Vector3[] traceSoundPoints;
    public Vector3[] TraceSoundPoints { get { return traceSoundPoints; } set { traceSoundPoints = value; } }
    Vector3 ForwardVector
    {
        get { return Enemy.transform.forward; }
    }
    private Vector3 alignDir;
    public Vector3 AlignDir { get { return alignDir; } set { alignDir = value; } }

    private Vector3 lookDir;
    public Vector3 LookDir { get { return lookDir; } set { lookDir = value; } }

    private Vector3 lockedDir; 
    public Vector3 LockedDir
    {
        get {
            if (Sight.LockInTarget == null)
                return Vector3.zero;
            return (transform.position - Sight.LockInTarget.transform.position).normalized; 
        }
    }

    #endregion
    #region 유한상태 머신 관련 
    private float pinIntervalTimer;
    public float PinIntervalTimer { get { return pinIntervalTimer; } set { pinIntervalTimer = value; } }
    private List<PatrolPoint> patrolPoints;
    public List<PatrolPoint> PatrolPoints { get { return patrolPoints; } set { patrolPoints = value; } }

    [SerializeField] private int patrolIndex; 
    public int PatrolIndex
    {
        get { return patrolIndex; }
        set { patrolIndex = value; }
    }
    #endregion
    private void Start()
    {
        patrolIndex = 0;
        DefaultMove = Instantiate(defaultMove);
        Auditory = GetComponent<SoundSensory>();
        Sight= GetComponent<SightSensory>();
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        patrolPoints = new List<PatrolPoint>();
        Enemy.CurrentStat.SyncMovementData(this); 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        DefaultMove = Instantiate(defaultMove);
        trail = GetComponent<TrailRenderer>();
    }

    public void ChangeMovementSpeed(float speed)
    {
        CurrentSpeed = Mathf.Lerp(currentSpeed, speed, 0.3f);
    }
    public void Rotator(Vector3 alignDir)
    {
        Quaternion rotation = Quaternion.LookRotation(alignDir);
        transform.rotation = rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);
        //LookDir = transform.forward; 
    }
    /// <summary>
    /// Will set the parameter unit vector as LookDir, and rotate to that location; 
    /// </summary>
    /// <param name="dir"></param>
    public void QuickRotator(Vector3 dir)
    {
        LookDir = dir; 
        Quaternion rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.5f);
    }

    public void Rotator()
    {
        Quaternion rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);
    }
    public void Rotator(float interval)
    {
        Quaternion rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);
    }

    public void RotateWhileRunning()
    {
        //TODO: 
    }
    public void Mover()
    {
        characterController.Move(currentSpeed * Time.deltaTime * LookDir);
    }
    public void Chase()
    {
        //QuickRotator(LookDir); 
        //characterController.Move(LookDir * currentSpeed * Time.deltaTime); 
        QuickRotator(LockedDir);
        characterController.Move(LockedDir * currentSpeed * Time.deltaTime);
    }

    public virtual void ReactToSound(Vector3[] newPath)
    {
        StopAllCoroutines();
        StartCoroutine(FollowSound(newPath));
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
    IEnumerator FollowSound(Vector3[] traceablePath)
    {
        traceSoundPoints = traceablePath;
        int trackingIndex = 0;
        Vector3 currentWaypoint = traceablePath[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                trackingIndex++;
                if (trackingIndex >= traceablePath.Length)
                {
                    Auditory.HaveHeard = false;
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            characterController.Move(currentWaypoint * currentSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (!debug && patrolPoints.Count >= 1)
        {
            for (int i = 0; i < patrolPoints.Count; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(patrolPoints[i].worldPosition, Vector3.one);

                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, patrolPoints[i].worldPosition);
                }
                else
                {
                    Gizmos.DrawLine(patrolPoints[i - 1].worldPosition, patrolPoints[i].worldPosition);
                }
            }
        }
    }
}