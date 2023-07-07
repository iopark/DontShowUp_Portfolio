using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Openable : MonoBehaviour
{
    [SerializeField] protected Transform openable;
    Transform player;
    [SerializeField] protected RectTransform picket;
    [SerializeField] protected Vector3 openVector;
    [SerializeField] protected Vector3 closeVector;
    protected Quaternion closeAngle;
    protected Quaternion openAngle;

    protected virtual void Awake()
    {
        closeAngle = Quaternion.Euler(closeVector);
        openAngle = Quaternion.Euler(openVector);
    }


    protected virtual void OpeningActivity()
    {

    }
}
