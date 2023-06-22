//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Act_Rotate_", menuName = "PluggableAI/Act/Rotate")]
//public class Rotate : Act
//{
//    public override void Perform(StateController controller)
//    {
//        Turn(controller); 
//    }

//    private void Turn(StateController controller) 
//    {
//        Quaternion rotation = Quaternion.LookRotation(controller.CurrentLookDir);
//        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, 0.3f); 
//    } 
//}
