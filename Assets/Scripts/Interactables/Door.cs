using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : Openable, IPointerEnterHandler, IPointerExitHandler, IInteractable
{
    #region Interaction Required Variables
    [SerializeField] protected Sound closingSound;
    float distanceToPlayer = default;
    float scaleRatio = default;
    float newRatio;
    const float minDistance = 5f;
    const float maxScale = 2f;
    const float minScale = 1f;
    public bool isOpened = false;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        //TODO: Declare sound here, openingSound = OpeningSound, Soundtype = SFX
        //TODO: Declare sound here, closingSound = ClosingSound, Soundtype = SFX
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.distance > minDistance)
            return;
        picket.gameObject.SetActive(true);
        picket.position = eventData.pointerCurrentRaycast.worldPosition;
        distanceToPlayer = eventData.pointerCurrentRaycast.distance;
        newRatio = distanceToPlayer / minDistance;
        AdjustPicketSize(newRatio);
        picket.localScale = Vector3.one * scaleRatio;
    }
    public void AdjustPicketSize(float ratio)
    {
        if (ratio < minScale)
        {
            picketCanvas.transform.localScale = Vector3.one; 
        }
        else if (ratio > maxScale)
        {
            picketCanvas.transform.localScale = Vector3.one * 2;
        }
        else 
        {
            scaleRatio = ratio;
            picketCanvas.transform.localScale = Vector3.one * scaleRatio;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (picket.gameObject.IsValid())
            picket.gameObject.SetActive(false);
    }
    [SerializeField] float openingTime = 0.5f;
    [SerializeField] float initialTime;
    #region Opening and Closing
    protected IEnumerator Open()
    {
        while (initialTime < openingTime)
        {
            initialTime += Time.deltaTime;
            openable.localRotation = Quaternion.Lerp(closeAngle, openAngle, initialTime / openingTime);
            yield return null;
        }
        initialTime = 0; 
        isOpened = true;
        openRoutine = null;
    }

    protected IEnumerator Close()
    {
        while (initialTime < openingTime)
        {
            initialTime += Time.deltaTime;
            openable.localRotation = Quaternion.Lerp(openAngle, closeAngle, initialTime / openingTime);
            yield return null;
        }
        initialTime = 0;
        isOpened = false;
        closeRoutine = null;
    }
    Coroutine openRoutine;
    Coroutine closeRoutine; 
    private void OpenDoor()
    {
        openRoutine = StartCoroutine(Open()); 
    }
    private void CloseDoor()
    {
        closeRoutine = StartCoroutine(Close());
    }
    public void Interact()
    {
        if (!ContestInteraction(distanceToPlayer))
            return;
        if (!isOpened)
        {
            OpenDoor();
            return;
        }
        else 
            CloseDoor();
    }

    public virtual bool ContestInteraction(float givenDist)
    {
        if (minDistance >= givenDist)
            return true;
        else
        {
            GameManager.CombatManager.CombatAlert("Need To Get Closer For Interaction");
            return false;
        }
    }
}
#endregion