using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TemporaryState Controller Inheriting NormalZombie Class
/// </summary>
public class StateController : NormalZombie
{
    //Utilizing Basic Zombie State Patterns, NormalZombie를 상속받은체로 진행한다. 
    public SightSensory Sight { get { return sight; } }
    public SoundSensory Auditory { get { return auditory; } }

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

    public bool patrolStatus;
    public bool searchCompleteStatus;

    [SerializeField] public CharacterController characterController;

    private void Awake()
    {
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

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            previousState = currentState;
            currentState = nextState;
        }
        return;
    }

    public override void ReactToSound(Vector3[] newPath)
    {
        base.ReactToSound(newPath);
    }

    protected override void ImportEnemyData()
    {
        base.ImportEnemyData();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

    }
}
