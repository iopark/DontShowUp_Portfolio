using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int speed;

    public bool debug;
    public bool tracingStatus;

    public Vector3[] tracePath;
    public int trackingIndex; 
    public void Listen(Transform trans)
    {
        throw new System.NotImplementedException();
    }

    public void ReactToSound(Vector3[] newPath)
    {
            StopAllCoroutines(); 
            StartCoroutine(FollowSound(newPath));
    }

    IEnumerator FollowSound(Vector3[] traceablePath)
    {
        tracePath = traceablePath;
        int trackingIndex = 0; 
        Vector3 currentWaypoint = traceablePath[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                trackingIndex++;
                if (trackingIndex >= traceablePath.Length)
                {
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    private void OnDrawGizmos()
    {
        if (!debug && tracingStatus)
        {
            for (int i = 0; i < tracePath.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(tracePath[i], Vector3.one);

                if (i == trackingIndex)
                {
                    Gizmos.DrawLine(transform.position, tracePath[i]);
                }
                else
                {
                    Gizmos.DrawLine(tracePath[i - 1], tracePath[i]);
                }
            }
        }
    }
}


