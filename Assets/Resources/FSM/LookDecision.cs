//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "LookDecision", menuName = "PluggableAI/Decisions/Look")]
//public class LookDecision : Decision
//{
//    //Ư���� ��ü�� ���� �ൿ�� ���ؼ� ��ũ���ͺ�� ������ �Ѵٸ�, 
//    //�ش� �ൿ�� ���ؼ� ��ũ���ͺ� ������Ʈ�������� �ൿ�� ���� �ɷ¹����� �������ټ��� ���� ������ 

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
