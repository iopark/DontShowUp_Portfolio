using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    Camera camera;
    PlayerInput playerInput; 
    Vector3 centrePoint;
    [SerializeField] LayerMask targetMask; 
    Vector3 middlePoint = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] float maxInteractDist; 
    [SerializeField] IInteractable interactable;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        camera = Camera.main;
    }

    private void Start()
    {
        GameManager.DataManager.PauseGame += PausePlayer; 
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

    private void PausePlayer()
    {
        if (playerInput.inputIsActive)
            playerInput.enabled = false;
        else 
            playerInput.enabled = true;
    }
    private void OnPause(InputValue value)
    {
        GameManager.DataManager.PauseGame?.Invoke(); 
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/PauseMenu"); 
    }
}
