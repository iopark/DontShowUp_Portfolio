using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            currentState.EnterStateAct(this);
            //currentState.EnterStateActions(this);
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
    public List<CoroutineSlip> coroutines = new List<CoroutineSlip>();
    Coroutine toDestroy;
    MoveRequestSlip currentRequest;
    MoveRequestSlip previousRequest; 
    bool isCompletingAction;
    public bool IsCompletingAction { get { return isCompletingAction; } }
    public void RequestMove(MoveType type, Vector3 destination, IEnumerator enumerator)
    {
        MoveRequestSlip newRequest = new MoveRequestSlip(type, destination, enumerator);
        if (actionRequests.Count > 0 && CheckForEqual(newRequest)) // if new request is considered an equal one, ignore this request. 
            return;
        actionRequests.Enqueue(newRequest);
        TryCompleteNext(); 
    }
    private void TryCompleteNext()
    {
        if (!isCompletingAction && actionRequests.Count > 0)
        {
            currentRequest = actionRequests.Dequeue();
            isCompletingAction = true;
            StartCoroutine(currentRequest.enumerator); 
        }
    }

    private bool CheckForEqual(MoveRequestSlip other)
    {
        foreach (MoveRequestSlip slip in  actionRequests)
        {
            if (other.moveType == slip.moveType)
                if (other.Equals(slip))
                    return true; 
        }
        return false;
    }
    /// <summary>
    /// Simply call this function for every coroutines to imply a designated action has been finished. 
    /// </summary>
    /// <param name="success"></param>
    public void FinishedAction(bool success)
    {
        if (!success)
        {
            //TODO: if some designated action is to fail, maybe allow the state to move to different state? if so, what should be placed in for the parameter?? 
            return;
        }
        //How do we Deliever this path to the Requestee? 
        else if (success)
        {
            //TODO: maybe do a next action? 
            isCompletingAction = false;
            TryCompleteNext(); // run the next expected coroutine 
        }
    }

    public void FinishedAction(bool success, System.Action<StateController> nextAction)
    {
        if (!success)
        {
            //TODO: if some designated action is to fail, maybe allow the state to move to different state? if so, what should be placed in for the parameter?? 
            return;
        }
        //How do we Deliever this path to the Requestee? 
        else if (success)
        {
            //TODO: maybe do a next action? 
            nextAction(this);
            isCompletingAction = false;
            TryCompleteNext(); // run the next expected coroutine 
        }
    }

    public void RunAndSaveForReset(string coroutineKey, IEnumerator _routine)
    {
        if (CheckOverlapCoroutine(coroutineKey, _routine))
            return; 
        Coroutine routine = StartCoroutine(_routine);
        CoroutineSlip newSlip = new CoroutineSlip(coroutineKey, routine);
        coroutines.Add(newSlip);
    }

    public void RemoveFromCoroutineList(string slipKey)
    {
        foreach (CoroutineSlip slip in coroutines)
        {
            if (slip.Equals(slipKey))
            {
                if (slip.routine != null)
                    StopCoroutine(slip.routine); 
                toDestroy = slip.routine; 
                coroutines.Remove(slip);
                return;
            }
        }
        toDestroy = null; 
    }

    private bool CheckOverlapCoroutine(string slipKey, IEnumerator _routine)
    {
        foreach (CoroutineSlip name in coroutines)
        {
            if (name.Equals(slipKey))
            {
                if (name.routine == null)
                {
                    StartCoroutine(_routine); 
                }
                return true;
            }
        }
        return false; 
    }

    /// <summary>
    /// if coroutine exists and if its not running, then stop the coroutine. 
    /// </summary>
    /// <param name="slipKey"></param>
    public void ResetCoroutine(string slipKey)
    {
        if (coroutines.Count == 0)
            return;
        foreach (CoroutineSlip name in coroutines)
        {
            if (name.Equals(slipKey))
            {
                if (name.routine != null)
                    StopCoroutine(name.routine);
                Debug.Log(name.routine);
                Debug.Log(name.coroutineKey); 
                //toDestroy = name.routine;
            }
            //toDestroy = null; 
        }
        Debug.Log("Coroutine did not exist"); 
        return;
    }

    public void ResetAllCoroutines()
    {
        if (coroutines.Count == 0)
            return; 
        foreach(CoroutineSlip slip in coroutines)
        {
            if (slip.routine == null)
                continue;
             StopCoroutine(slip.routine);
        }
        //coroutines.Clear();
    }

    #endregion
    protected void OnDrawGizmos()
    {
        Gizmos.color = currentState.sceneGizmoColor;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}