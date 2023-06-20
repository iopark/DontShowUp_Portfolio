using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FSMState : ScriptableObject
{
    protected StateController owner;
    [SerializeField] protected FSMState[] transitionStates;
    //Animation Triggers for each State 
    protected string enterAnimation;
    public string EnterAnimation => enterAnimation;

    protected string exitAnimation;
    public string ExitAnimation => exitAnimation; 

    public FSMState(StateController owner)
    {
        this.owner = owner;
    }

    public FSMState(StateController owner, string enterAnim, string exitAnim)
    {
        this.owner = owner;
        this.enterAnimation = enterAnim;
        this.exitAnimation = exitAnim;
    }
    public abstract void Initialize();
    public abstract void Enter();
    public abstract void Update();
    public abstract void PhysicalUpdate(); // For the physical updates which may depend fixedUpdate 
    public virtual void Exit() { Reset(); } // default Exit 
    public abstract void UpdateAnimation(string animTrigger); 
    //Maybe simply make this called upon transition? 
    public abstract void Reset(); //Determine variables which may require resetting with reactivation in mind 

}
