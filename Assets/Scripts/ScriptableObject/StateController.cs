using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TemporaryState Controller Inheriting NormalZombie Class
/// </summary>
public class StateController : NormalZombie
{
    //Utilizing Basic Zombie State Patterns, NormalZombie�� ��ӹ���ü�� �����Ѵ�. 
    public SightSensory Sight { get { return sight; } }
    public SoundSensory Auditory { get { return auditory; } }

    #region Required Variables to Control States 
    private int patrolIndex;
    public int PatrolIndex 
    { 
        get { return patrolIndex; }
        set { 
            if (value > patrolIndex)
                searchCompleteStatus = true;
            patrolIndex = value; 
        } 
    }
    public int patrolCount;

    public State currentState;
    public State remainState;
    public State previousState;
    private Vector3 currentLookDir; 
    public Vector3 CurrentLookDir
    {
        get { return currentLookDir; }
        set { currentLookDir = value; }
    }
    private Vector3 targetDir;
    public Vector3 TargetDir
    {
        get { return targetDir; }
        set { targetDir = value; }
    }

    private Vector3 fixToDir; 
    public Vector3 FixToDir
    {
        get { return fixToDir; }
        set { fixToDir = value; }
    }

    private float currentSpeed; 
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    //Search and Patrol Segments 
    public List<PatrolPoint> patrolPoints; 
    public bool patrolStatus;
    public bool searchCompleteStatus;
    private float elapsedTime; 
    public float ElapsedTime
    {
        get { return elapsedTime; }
        set { elapsedTime = value; }
    }
    #endregion
    protected override void Awake()
    {
        base.Awake(); 
        patrolPoints = new List<PatrolPoint>();
        elapsedTime = 0;
        currentSpeed = MoveSpeed; 
        patrolIndex = 0;
        patrolCount = 0;
        searchCompleteStatus = false;
        patrolStatus = false;
        //characterFov = GetComponent<FieldOfView>();
        characterController = GetComponent<CharacterController>();

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
            EnterState(); 
            AnimationUpdate(currentState.EnterStateAnim().Item1, currentState.EnterStateAnim().Item2); 
        }
        return;
    }

    private void EnterState()
    {
        currentState.EnterState(this);
    }
    private void AnimationUpdate(int animType, string animKeyword)
    {
        //TODO: Each state should be able to update the Statemachine's Animation as well 
        //Where default is the Animation type trigger
        switch(animType)
        {
            case 1: 
                if (animKeyword == "Walk")
                    anim.SetFloat("Speed", CurrentStat.moveSpeed); break;
            default: anim.SetTrigger(animKeyword); break;
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public bool CheckElapsedTime(float time)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            return true;
        }

        return false;
    }

    IEnumerator Attack(float attackInterval) 
    { 
        float timer = Time.deltaTime; 
        while (true) 
        { 
            if (timer>= attackInterval) { 
                //something to return to let delegate pattern identify whether this is passed. 
            }
                yield break; 
        }

    }
}