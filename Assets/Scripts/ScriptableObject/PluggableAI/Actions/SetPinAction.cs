//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Unity.VisualScripting;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Action_SetPin_", menuName = "PluggableAI/Actions/SetPin")]
//public class SetPinAction : Action
//{
//    [SerializeField] private float setPinInterval; 
//    [SerializeField] private Act defaultMove; //Move
//    public override void Act(StateController controller)
//    {
//        SetPin(controller);
//    }

//    protected virtual void SetPin(StateController controller)
//    {
//        //Set a new Search Point, and then 
//        defaultMove.Perform(controller);
//        if (controller.CheckElapsedTime(setPinInterval))
//        {
//            controller.ElapsedTime = 0;
//            PatrolPoint pin = new PatrolPoint(controller.transform.forward, controller.transform.position);
//            controller.patrolPoints.Add(pin); 
//        }
//    }
//}
