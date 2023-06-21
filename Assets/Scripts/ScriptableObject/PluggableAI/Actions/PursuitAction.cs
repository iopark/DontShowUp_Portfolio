using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Pursuit")]
public class PursuitAction : Action
{
    [SerializeField] private float speed;
    public override void Act(StateController controller)
    {
        Pursuit(controller);
    }

    private void Pursuit(StateController controller)
    {
        Vector3 target = controller.Sight.FindTarget();
        if (target != Vector3.zero)
        {
            Vector3 lookDir = (target - controller.transform.position).normalized;
            lookDir.y = controller.transform.position.y;
            controller.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            controller.characterController.Move(lookDir * speed * Time.deltaTime);
            controller.anim.SetBool("Walk Forward", true);
        }
    }

    //private void Trace(StateController controller, Vector3 target)
    //{
    //    lookDir = (target - body.transform.position).normalized;
    //    lookDir.y = body.transform.position.y;
    //    body.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
    //    body.Move(lookDir * speed * Time.deltaTime);
    //    anim.SetBool("Walk Forward", true);
    //}

}
