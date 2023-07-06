using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SoundMaker : MonoBehaviour
{
    public float range; 
    // Start is called before the first frame update
    public void TriggerSound(float range)
    {
        this.range = range;

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {

            IListenable listener = collider.GetComponent<IListenable>();
            listener?.Heard(transform.position, WallIntersect(collider.transform.position));
        }
        
    }

    private bool WallIntersect(Vector3 destination)
    {
        Vector3 dir = destination - transform.position;
        return Physics.Raycast(transform.position, dir.normalized, dir.sqrMagnitude, LayerMask.GetMask("Unwalkable")); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); 
    }
}
