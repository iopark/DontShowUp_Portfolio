using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    Camera camera;
    [SerializeField] PhysicsRaycaster raycaster; 
    Vector3 centrePoint;
    [SerializeField] LayerMask targetMask; 
    Vector3 middlePoint = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] float maxInteractDist; 
    [SerializeField] IInteractable interactable;

    private void Awake()
    {
        camera = Camera.main;
        raycaster = camera.gameObject.GetComponent<PhysicsRaycaster>();
    }

    RaycastHit[] hitList = null;
    private void TryToInteract()
    {
        centrePoint = camera.ScreenToWorldPoint(middlePoint);
        Debug.DrawRay(centrePoint, camera.transform.forward, Color.blue, 10f);
        hitList = Physics.RaycastAll(centrePoint, camera.transform.forward, maxInteractDist, targetMask);
        if (hitList == null)
            return; 
        foreach (RaycastHit hit in hitList)
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
    private void OnInteract(InputValue value)
    {
        TryToInteract(); 
    }
}
