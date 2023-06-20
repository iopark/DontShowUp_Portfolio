using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateController : StateController
{
    public enum State
    {
        Idle, 
        Search, 
        Patrol, 
        Alert, 
        Size
    }
    #region Drag and Drop the Scriptable States 
    [SerializeField] private IdleState idleState;
    [SerializeField] private SearchState searchState;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private AlertState alertState;
    public FSMState[] stateList; 

    public IdleState IdleState { get { return idleState; }  }
    public SearchState SearchState { get { return searchState; } }
    public PatrolState PatrolState { get {  return patrolState;} }
    public AlertState AlertState { get { return alertState;} }
    #endregion
    #region GetSet Properties for Body Components 
    public SoundSensory Aud { get { return aud; } }
    public SightSensory Fov { get { return fov; } }

    public CharacterController _CharacterController {  get { return characterController; } }
    #endregion

    public bool isAttacking;
    public bool isTracing; 

    private void Awake()
    {
        stateList = new FSMState[(int)State.Size]; 
        //Instantiate all the required 
        idleState = GameManager.Resource.Instantiate(idleState);
    }
    protected override void ChangeState(FSMState state)
    {
        //How should Zombie Change different States?

        base.ChangeState(state);
    }

    protected override void UpdateAnim(string anim)
    {
        this.anim.SetTrigger(anim); // trigger the transitioning anim state's animation 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.currentState.Update();
    }
}
