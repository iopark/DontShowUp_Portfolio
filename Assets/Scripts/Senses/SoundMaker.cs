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
            listener?.Heard(transform.position);
            
            //Return the sight which should print out tracable path for the listener. 
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); 
    }
}