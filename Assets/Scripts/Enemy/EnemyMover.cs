using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;

public enum MoveState
{
    Idle,
    Normal, 
    Alert, 
    Attack
}
public class EnemyMover : MonoBehaviour
{
    [Header("Debugging Purposes")]
    public Vector3 currentDestination;
    public bool debug; 

    CharacterController characterController;
    SightSensory Sight { get; set; }
    Animator animator;
    Enemy Enemy { get; set; }
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
    [SerializeField] private Vector3[] traceSoundPoints;
    public Vector3[] TraceSoundPoints { get { return traceSoundPoints; } set { traceSoundPoints = value; } }

    private Vector3 lookDir;
    public Vector3 LookDir { get { return lookDir; } set { lookDir = value; } }

    private float distanceToTarget;

    #endregion
    #region 유한상태 머신 관련 
    [SerializeField] private int patrolCount;
    public int PatrolCount { get { return patrolCount; } set { patrolCount = value; } }

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
    private bool isTracingSound; 
    public bool IsTracingSound
    {
        get { return isTracingSound; }
        set { isTracingSound = value; }
    }
    #endregion

    #region 움직임 계산 관련 
    public const float dotThreshold = 0.98f;
    public const float distanceThreshhold = 0.1f; 
    Quaternion rotation; 
    private void Start()
    {
        patrolIndex = 0;        
        characterController = GetComponent<CharacterController> ();
        Sight = GetComponent<SightSensory>();
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        patrolPoints = new List<PatrolPoint>();
        Enemy.CurrentStat.SyncMovementData(this); 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    float nextSpeed; 
    /// <summary>
    /// Insert Movement Type, 
    /// Normal, 
    /// Alert, 
    /// and Attack using MoveState 
    /// </summary>
    /// <param name="speed"></param>
    public void ChangeMovementSpeed(MoveState mState) 
    {
        switch (mState)
        {
            case MoveState.Normal: nextSpeed = normalMoveSpeed; break;
            case MoveState.Alert: nextSpeed = alertMoveSpeed; break;
            default: nextSpeed = 0f; break;
        }
        CurrentSpeed = nextSpeed; 
    }

    public void Rotator(Vector3 alignDir)
    {
        rotation = Quaternion.LookRotation(alignDir);
        transform.rotation = rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);
    }

    public void Rotator()
    {
        rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = rotation; //Quaternion.Lerp(transform.rotation, rotation, 1f);
    }
    public void Rotator(float interval)
    {
        rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, interval);
    }

    public void RotateWhileRunning()
    {
        //TODO: 
    }
    public void Mover()
    {
        characterController.Move(currentSpeed * Time.deltaTime * LookDir);
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
    #endregion
    protected virtual void OnDrawGizmos()
    {
        if (!debug && traceSoundPoints.Length > 0)
        {
            for (int i = 0; i < traceSoundPoints.Length; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawCube(traceSoundPoints[i], Vector3.one);

                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, traceSoundPoints[i]);
                }
                else
                {
                    Gizmos.DrawLine(traceSoundPoints[i - 1], traceSoundPoints[i]);
                }
            }
        }

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