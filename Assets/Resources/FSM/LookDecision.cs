//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "LookDecision", menuName = "PluggableAI/Decisions/Look")]
//public class LookDecision : Decision
//{
//    //특정한 객체에 대한 행동에 대해서 스크립터블로 적용을 한다면, 
//    //해당 행동에 대해서 스크립터블 오브젝트에서또한 행동에 따른 능력범위를 산정해줄수도 있지 않을까 

//    [SerializeField] private float range;
//    [SerializeField] private float sightRange;
//    [SerializeField, Range(0, 360f)] float sightAngle;
//    [SerializeField] LayerMask targetMask;
//    [SerializeField] LayerMask obstacleMask;

//    public override bool Decide(StateController controller)
//    {
//        bool targetVisible = Look(controller); 
//        return targetVisible;
//    }

//    private bool Look(StateController controller)
//    {
//        Vector3 findingTarget = controller.characterFov.FindTarget();
//        return findingTarget != Vector3.zero; 
//    }
//}
