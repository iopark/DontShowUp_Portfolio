using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
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

    Coroutine highlight;
    Coroutine exit; 
    IEnumerator PointerHighlighted()
    {
        transitionTime = 0;
        while (transitionTime < highlightTime)
        {
            transitionTime += Time.deltaTime;
            button.image.fillAmount = Mathf.Lerp(0, 1, transitionTime / highlightTime); 
            yield return null;
        }
        //highlight = null;
    }

    IEnumerator PointerExiting()
    {
        transitionTime = 0;
        while (transitionTime < highlightTime)
        {
            transitionTime += Time.deltaTime;
            button.image.fillAmount = Mathf.Lerp(1, 0, transitionTime / highlightTime);
            yield return null;
        }
        //exit = null;
    }
}
