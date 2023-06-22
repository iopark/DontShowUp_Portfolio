using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Act_Wander_", menuName = "PluggableAI/Act/Wander")]
public class Wander : Act
{
    [SerializeField] private float rangeMin;
    [SerializeField] private float rangeMax;
    public override void Perform(StateController controller)
    {
        SelectWanderSpot(controller);
    }

    private void SelectWanderSpot(StateController controller)
    {
        float x = Random.Range(rangeMin, rangeMax);
        float y = Random.Range(rangeMin, rangeMax);
        controller.CurrentLookDir = new Vector3(x, 0f, y).normalized;
    }
}
