using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Button button;
    public const float deltTime = 0.0167f; 
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => StopAllCoroutines());
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight = StartCoroutine(PointerHighlighted());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        exit = StartCoroutine(PointerExiting());
    }
    float transitionTime; 
    const float highlightTime = 0.2f;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    Coroutine highlight;
    Coroutine exit; 
    IEnumerator PointerHighlighted()
    {
        transitionTime = 0;
        while (transitionTime < highlightTime)
        {
            transitionTime += deltTime; 
            button.image.fillAmount = Mathf.Lerp(0, 1, transitionTime / highlightTime); 
            yield return null;
        }
    }

    IEnumerator PointerExiting()
    {
        transitionTime = 0;
        while (transitionTime < highlightTime)
        {
            transitionTime += deltTime; 
            button.image.fillAmount = Mathf.Lerp(1, 0, transitionTime / highlightTime);
            yield return null;
        }
    }
}
