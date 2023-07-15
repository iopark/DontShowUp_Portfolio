using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : Openable, IPointerEnterHandler, IPointerExitHandler, IInteractable
{
    [SerializeField] float distanceToPlayer = default; 
    float scaleRatio = default;
    float newRatio; 
    const float minDistance = 5f;
    const float maxScale = 2f;
    const float minScale = 1f;
    public bool isOpened; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Touched");
        Debug.Log(eventData.pointerCurrentRaycast.distance); 
        if (eventData.pointerCurrentRaycast.distance > minDistance)
            return;
        picket.gameObject.SetActive(true); 
        picket.position = eventData.pointerCurrentRaycast.worldPosition;
        distanceToPlayer = eventData.pointerCurrentRaycast.distance;
        newRatio = distanceToPlayer/ minDistance;
        AdjustPicketSize(newRatio); 
        picket.transform.localScale = Vector3.one * scaleRatio;
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

    protected override void Awake()
    {
        base.Awake();
        //GameManager.SceneManager.introScene += Dangling; 
    }
    Coroutine openRoutine; 
    float openingTime = 0.5f;
    float initialTime;
    protected IEnumerator OpenBox()
    {
        while (initialTime < openingTime)
        {
            initialTime += Time.deltaTime;
            openable.localRotation = Quaternion.Lerp(closeAngle, openAngle, initialTime / openingTime);
            yield return null;
        }
        isOpened = true;
        GameManager.DataManager.Diamond++; 
    }

    public void Interact()
    {
        if (distanceToPlayer == default || !ContestInteraction(distanceToPlayer))
            return;
        if (!picket.gameObject.IsValid())
            return;
        picket.gameObject.SetActive(false); 
        openRoutine = StartCoroutine(OpenBox());
    }

    public void GiveDiamond()
    {
        GameManager.DataManager.Diamond++; 
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