using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyMoverSound : MonoBehaviour
{
    [Header("Debugging Purposes")]
    public Vector3 currentDestination;
    public Vector3[] traceSoundPoints; 
    public bool debug; 

    SoundSensory SoundSensory { get; set; }
    Animator animator;
    Enemy Enemy { get; set; }
    [SerializeField] Act defaultMove; //Scriptable Object to Instantiate and put to use. 
    public Act DefaultMove { get; private set; }
    public float moveSpeed = 10f; 
    private void Start()
    {
        SoundSensory = GetComponent<SoundSensory>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    

    public virtual void ReactToSound(Vector3[] newPath)
    {
        StopAllCoroutines();
        StartCoroutine(FollowSound(newPath));
    }
    IEnumerator FollowSound(Vector3[] traceablePath)
    {
        traceSoundPoints = traceablePath;
        int trackingIndex = 0;
        Vector3 currentWaypoint = traceablePath[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                trackingIndex++;
                if (trackingIndex >= traceablePath.Length)
                {
                    SoundSensory.HaveHeard = false;
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (!debug && traceSoundPoints.Length > 0)
        {
            for (int i = 0; i < traceSoundPoints.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(traceSoundPoints[i], Vector3.one);

                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, traceSoundPoints[i]);
                }
                else
                {
                    Gizmos.DrawLine(traceSoundPoints[i - 1], traceSoundPoints[i]);
                }
            }
        }
    }
}