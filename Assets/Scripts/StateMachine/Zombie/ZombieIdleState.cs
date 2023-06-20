using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle_ZombieType_", menuName = "PluggableAI/Zombie/Idle")]
public class ZombieIdleState : FSMState
{
    public string animEnter = "IdleState"; 
    public ZombieIdleState(StateController owner) : base(owner)
    {
    }

    public override void Enter()
    {

    }

    public override void Initialize()
    {
        //Utilize owner.Sight to adjust the 
    }

    public override void PhysicalUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        //TODO: Condition in which should they change to 
        //TODO: Search State 
        //TODO: Patrol State 
        //TODO: Attack State 
        throw new System.NotImplementedException();
    }

    public override void UpdateAnimation(string animTrigger)
    {
        
    }
    //TODO: Based on the wall's basis Vector, should orient itself to either .right or .-right unit vector 


}
