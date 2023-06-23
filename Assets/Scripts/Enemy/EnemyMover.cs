using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Act))]
public class EnemyMover : MonoBehaviour
{
    CharacterController characterController;
    SoundSensory SoundSensory { get; set; }
    Animator animator;
    Enemy Enemy { get; set; }
    [SerializeField] Act defaultMove; //Scriptable Object to Instantiate and put to use. 
    public Act DefaultMove { get; private set; }
    #region Pertaining to Move 
    //Moving Abilities 
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            animator.SetFloat("Speed", value);
            moveSpeed = value;
        }
    }
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
    #endregion
    #region 유한상태 머신 관련 
    private float pinIntervalTimer;
    public float PinIntervalTimer { get { return pinIntervalTimer; } set { pinIntervalTimer = value; } }
    private List<PatrolPoint> patrolPoints;
    public List<PatrolPoint> PatrolPoints { get { return patrolPoints; } set { patrolPoints = value; } }
    #endregion
    private void Start()
    {
        DefaultMove = GameManager.Resource.Instantiate(defaultMove);
        SoundSensory = GetComponent<SoundSensory>();
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        patrolPoints = new List<PatrolPoint>();
    }


    private Vector3 lookDir; 
    public Vector3 LookDir {  get { return lookDir; } set {  lookDir = value; } }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        DefaultMove = GameManager.Resource.Instantiate(defaultMove);
    }

    public void Rotator()
    {
        Quaternion rotation = Quaternion.LookRotation(LookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);
    }

    public void RotateWhileRunning()
    {
        //TODO: 
    }
    public void Mover(Vector3 destPoint)
    {
        characterController.Move(moveSpeed * Time.deltaTime * destPoint);
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
                    SoundSensory.HaveHeard = false;
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            characterController.Move(currentWaypoint * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
