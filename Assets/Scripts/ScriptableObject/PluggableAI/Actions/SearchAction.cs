using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_", menuName = "PluggableAI/Actions/Search")]
public class SearchAction : Action
{
    [SerializeField] private float searchSpeed;
    [SerializeField] private float searchRange;
    public override void Act(StateController controller)
    {
        Search(controller);
    }

    protected virtual void Search(StateController controller)
    {
        if (!controller.searchStatus)
        {
            controller.searchStatus = true;
            Vector3 searchPoint = (Vector3)UnityEngine.Random.insideUnitCircle;
            controller.returnPoints.Add(searchPoint);
        }
        Approach(controller, controller.returnPoints[controller.returnPoints.Count - 1]);
    }

    private void Approach(StateController controller, Vector3 searchLoc)
    {
        Vector3 lookDir = (searchLoc - controller.transform.position).normalized;
        lookDir.y = 0;
        controller.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
        controller.characterController.Move(lookDir * searchSpeed * Time.deltaTime);
        controller.characterFov.anim.SetBool("Walk Forward", true);
    }
    IEnumerator Searching()
    //{

    //}

    private void ReInitialize()
    {

    }
}
