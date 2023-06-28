//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Decision_Align_", menuName = "PluggableAI/Decisions/Align")]
//public class AlignDecision : Decision
//{
//    [SerializeField] private LayerMask wallLayer;
//    public override bool Decide(StateController controller)
//    {
//        return Align(controller);
//    }
//    private bool Align(StateController controller)
//    {
//        if (!controller.IsCompletingAction || controller.currentMoveType == MoveType.RotateOnly)
//        ////Determine and return true when subject has aligned properly (parallel to the wall. )
//        //if (Vector3.Dot(controller.CurrentLookDir, controller.EnemyMover.AlignDir) > 0.99f)
//        {
//        //    controller.EnemyMover.AlignDir = Vector3.zero; 
//        //    return true;
//        //}
//        ////this should return false and continue to do so, 
//        //return false;
//    }
//}
