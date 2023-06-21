using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Act_Move_", menuName = "PluggableAI/Act/Move")]
public class Move : Act
{
    //Can be Configurable based on the speed; 
    [SerializeField] private string animTrigger; 
    public override void Perform(StateController controller)
    {
        OnMove(controller); 
    }

    private void OnMove(StateController controller)
    {
        controller.characterController.Move(controller.CurrentLookDir * controller.CurrentSpeed * Time.deltaTime); 
    }
}
