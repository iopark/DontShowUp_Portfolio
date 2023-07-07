using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : Openable, IPointerEnterHandler, IPointerExitHandler, IInteractable
{
    #region Interaction Required Variables
    float distanceToPlayer;
    float scaleRatio = default;
    float newRatio;
    const float minDistance = 2.5f;
    const float maxScale = 2f;
    const float minScale = 1f;
    public bool isOpened = false; 
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        picket.gameObject.SetActive(true);
        picket.position = eventData.pointerCurrentRaycast.worldPosition;
        distanceToPlayer = eventData.pointerCurrentRaycast.distance;
        newRatio = minDistance / distanceToPlayer;
        AdjustPicketSize(newRatio);
        picket.localScale = Vector3.one * scaleRatio;
    }

    public void AdjustPicketSize(float ratio)
    {
        if (ratio < minScale)
            scaleRatio = minScale;
        else if (ratio > maxScale)
            scaleRatio = maxScale;
        else
            scaleRatio = ratio;
        picket.localScale *= scaleRatio;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (picket.gameObject.IsValid())
            picket.gameObject.SetActive(false);
    }

    [SerializeField] float openingTime = 0.5f;
    [SerializeField] float initialTime;

    // Start is called before the first frame update

    protected IEnumerator Open()
    {
        while (initialTime < openingTime)
        {
            initialTime += Time.deltaTime;
            openable.rotation = Quaternion.Lerp(closeAngle, openAngle, initialTime / openingTime);
            yield return null;
        }
        isOpened = true;
        openRoutine = null;
    }

    IEnumerator Close()
    {
        while (initialTime < openingTime)
        {
            initialTime += Time.deltaTime;
            openable.rotation = Quaternion.Lerp(openAngle, closeAngle, initialTime / openingTime);
            yield return null;
        }
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
        if (ContestInteraction(distanceToPlayer))
        if (!isOpened)
            OpenDoor();
        CloseDoor();
    }

    public bool ContestInteraction(float givenDist)
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
