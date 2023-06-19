using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public void TriggerSound(Transform transform, float range)
    {
        //Vector3 mapSoundOrigin = Extension.ConvertWorldtoMapPoint(transform); 
        //Vector3 mapSoundOrigin = GameManager.Map.mapPos.InverseTransformPoint(transform.position); 
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            IListenable listener = collider.GetComponent<IListenable>();
            Cell listenerPosition = listener?.ReturnHeardPosition();
            if (listenerPosition != null )
            {
                //If Sound was made, and it was listenable for the audience, 
                //audience should request for the path no? 
                //or is it more like sould can be delivered to the audience? 

                GameManager.PathManager.RequestPath(listenerPosition.worldPos, transform.position, listener.GetPath); 
            }
            //Return the sight which should print out tracable path for the listener. 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f); 
    }
}
