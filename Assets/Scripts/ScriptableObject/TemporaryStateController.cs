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

    public int patrolIndex;
    public int patrolCount;
    public State currentState;
    public State remainState;
    public State previousState;
    public EnemyStat enemyStat;

    public bool patrolStatus;
    public bool searchStatus;

    [SerializeField] public CharacterController characterController;

    private void Awake()
    {
        patrolIndex = 0;
        patrolCount = 0;
        searchStatus = false;
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
