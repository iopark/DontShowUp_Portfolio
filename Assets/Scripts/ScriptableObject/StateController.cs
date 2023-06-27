using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TemporaryState Controller Inheriting NormalZombie Class
/// </summary>
public class StateController : MonoBehaviour
{
    #region For the Convenience of State Machine Interaction, GetSet Linked with different Unit Bodies 
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return EnemyMover.CurrentSpeed; }
        set { EnemyMover.CurrentSpeed = value; }
    }

    public Vector3 ForwardVector
    {
        get { return Enemy.transform.forward; }
    }

    private Vector3 currentLookDir;
    public Vector3 CurrentLookDir
    {
        get { return EnemyMover.LookDir; }
    }
    public List<PatrolPoint> PatrolPoints
    {
        get { return EnemyMover.PatrolPoints;  }
        set { EnemyMover.PatrolPoints = value; }
    }

    public int PatrolIndex
    {
        get { return EnemyMover.PatrolIndex; }
        set { EnemyMover.PatrolIndex = value; } 
    }
    #endregion
    #region Required Variables for State Controller 

    public int patrolCount;
    //Search and Patrol Segments 
    public bool patrolStatus;
    public bool searchCompleteStatus;

    #endregion
    #region GetSet Unitbodies, must be declared on the Awake 
    public Enemy Enemy { get; private set; }
    public EnemyAttacker EnemyAttacker { get; private set; }
    public EnemyMover EnemyMover { get; private set; }
    public SightSensory Sight { get; private set; }
    public SoundSensory Auditory { get; private set; }
    #endregion

    [Header("Unit State")]
    public State currentState;
    public State remainState;
    public State previousState;

    //[Header("StateRequired Elements")]
    //public bool isAligning; 
    //public 
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        EnemyAttacker = GetComponent<EnemyAttacker>();
        EnemyMover = GetComponent<EnemyMover>();
        Sight = GetComponent<SightSensory>();
        Auditory = GetComponent<SoundSensory>();

        //DefaultAttack = Instantiate(defaultAttack);
        patrolCount = 0;
        searchCompleteStatus = false;
        patrolStatus = false;
    }


    private void Update()
    {
        currentState.UpdateState(this);
    }
    private void FixedUpdate()
    {
        //currentState.FixedUpdateState(this);
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState.ExitState(this);
            previousState = currentState;
            currentState = nextState;
            name = currentState.name;
            AnimationUpdate(); 
            currentState.EnterState(this);
        }
        return;
    }

    public void AnimEvent()
    {

    }
    public void AnimationUpdate()
    {
        AnimRequestSlip? stateAnim = currentState.EnterStateAnim();
        if (stateAnim == null)
            return;
        Enemy.AnimationUpdate((AnimRequestSlip)stateAnim); 
    }
    #region attempting to perform request for future path in delegated ways
    public Queue<MoveRequestSlip> actionRequests = new Queue<MoveRequestSlip>();
    MoveRequestSlip currentRequest;
    bool isCompletingAction;
    public void RequestMove(Vector3 from, Vector3 to)
    {
        MoveRequestSlip newRequest;
        if (to == Vector3.zero)
        {
            newRequest = new MoveRequestSlip(from);
        }
        else 
            newRequest = new MoveRequestSlip(from, to);
        actionRequests.Enqueue(newRequest);
        TryCompleteNext(); 
    }
    void TryCompleteNext()
    {
        if (!isCompletingAction && actionRequests.Count > 0)
        {
            currentRequest = actionRequests.Dequeue();
            isCompletingAction = true; 
            if (currentRequest.moveType == MoveType.RotateOnly)
            {
                //TODO: Run the Coroutine 
                return;
            }

        }
    }

    public void FinishedAction(bool success)
    {
        if (success) // path �� ã�������� �ش� Path �� �����Ѵ�. 
            currentRequest.callback(success);
        //How do we Deliever this path to the Requestee? 

        isCompletingAction = false;
        TryCompleteNext();
    }
    #endregion
    protected void OnDrawGizmos()
    {
        Gizmos.color = currentState.sceneGizmoColor;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}