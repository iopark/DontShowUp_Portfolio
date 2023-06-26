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
    public Queue<ActionRequestSlip> actionRequests = new Queue<ActionRequestSlip>();
    ActionRequestSlip currentRequest;
    bool isCompletingAction;
    public void RequestAction(UnityEngine.Object bodyComponent, float interval, Action<bool> _callback)
    {
        ActionRequestSlip newRequest = new ActionRequestSlip(bodyComponent, interval, _callback);
        actionRequests.Enqueue(newRequest);
        TryCompleteNext(); 
    }
    void TryCompleteNext()
    {
        if (!isCompletingAction && actionRequests.Count > 0)
        {
            currentRequest = actionRequests.Dequeue();
            isCompletingAction = true; 
            Enemy.DoAction(currentRequest.bodyComponent, currentRequest.interval);
        }
    }

    public void FinishedProcessingPath(bool success)
    {
        if (success) // path 를 찾았을때만 해당 Path 를 전달한다. 
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