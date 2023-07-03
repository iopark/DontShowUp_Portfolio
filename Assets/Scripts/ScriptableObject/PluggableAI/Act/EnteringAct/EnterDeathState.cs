using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnterAct_DeathState_", menuName = "PluggableAI/EnterAct/DeathState")]
public class EnterDeathState : Act
{
    public override void Perform(StateController controller)
    {
        //TODO: Stop all the running coroutines
        controller.StopAllCoroutines();
        GameManager.DataManager.PlayerKills++;
        controller.Enemy.UponDeath(); 
    }
}
