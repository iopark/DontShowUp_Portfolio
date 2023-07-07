using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] LayerMask interactMask; 
    Camera camera;
    Vector3 centrePoint;
    Vector3 middlePoint = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] float checkDistance; 
    [SerializeField] GameObject interactableItem; 
    [SerializeField] IInteractable interactable;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        CheckInteraction(); 
    }

    private void CheckInteraction()
    {
        centrePoint = camera.ScreenToWorldPoint(middlePoint);
        Debug.DrawRay(centrePoint, camera.transform.forward, Color.gray);
        if (Physics.Raycast(centrePoint, camera.transform.forward, out RaycastHit hitInfo, checkDistance, interactMask))
        { 
            interactable = hitInfo.collider.GetComponent<IInteractable>();
            interactableItem = hitInfo.collider.gameObject; 
        }
    }
    private void TryToInteract()
    {
        // only enable Interaction when the distance is less or equal to 2.5f; 
        interactable = interactableItem.GetComponent<IInteractable>();
        interactable?.Interact();
    }

    private void OnInteract(InputValue value)
    {
        if (interactableItem == null)
            return;
        TryToInteract();
    }
}
