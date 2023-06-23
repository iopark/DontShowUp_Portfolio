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
        get { return EnemyMover.MoveSpeed; }
        set { EnemyMover.MoveSpeed = value; }
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
    #endregion
    #region Required Variables for State Controller 
    private int patrolIndex;
    public int PatrolIndex
    {
        get { return patrolIndex; }
        set
        {
            if (value > patrolIndex)
                searchCompleteStatus = true;
            patrolIndex = value;
        }
    }
    public int patrolCount;
    //Search and Patrol Segments 
    public bool patrolStatus;
    public bool searchCompleteStatus;
    public Queue<ActionRequestSlip> actionRequests = new Queue<ActionRequestSlip>();
    ActionRequestSlip currentRequest; 
    bool isCompletingAction; 
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
        patrolIndex = 0;
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
        currentState.FixedUpdateState(this);
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            previousState = currentState;
            currentState = nextState;
            name = currentState.name;
            EnterState();
            AnimationUpdate(currentState.EnterStateAnim().Item1, currentState.EnterStateAnim().Item2);
        }
        return;
    }

    public void AnimEvent()
    {

    }
    private void EnterState()
    {
        currentState.EnterState(this);
    }
    private void AnimationUpdate(int animType, string animKeyword)
    {
        //TODO: Each state should be able to update the Statemachine's Animation as well 
        //Where default is the Animation type trigger
        switch (animType)
        {
            case 1:
                if (animKeyword == "Walk")
                    Enemy.anim.SetFloat("Speed", Enemy.CurrentStat.moveSpeed); break;
            default: Enemy.anim.SetTrigger(animKeyword); break;
        }
    }
    #region attempting to perform request for future path in delegated ways
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