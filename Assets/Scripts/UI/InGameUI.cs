using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : BaseUI
{
    public Slider slider; 
    Camera cam = Camera.main;

    public Transform followTarget;
    public Vector3 followOffset;

    float maxScale = 2f; 


    protected virtual void LateUpdate()
    {
        if (followTarget != null)
        {
            transform.position = cam.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }

    public void SetTarget(Transform target)
    {
        followTarget = target;
        if (followTarget != null)
        {
            transform.position = cam.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }

    public void SetOffset(Vector3 offset)
    {
        followOffset = offset;
        if (followTarget != null)
        {
            transform.position = cam.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }

    public override void CloseUI()
    {
        base.CloseUI();

        GameManager.UIManager.CloseInGameUI(this);
    }
}