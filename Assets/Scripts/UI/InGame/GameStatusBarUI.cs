using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusBarUI : SceneUI
{
    [SerializeField] float scaleSize = 1.3f;
    [SerializeField] float totalscaletime = 0.1f;

    Queue<TMP_Text> alerts = new Queue<TMP_Text>();
    private int messageCount = 1; 
    public int MessageCount
    {
        get { return messageCount; }
        set
        {
            if (value > 5)
                messageCount = 1; 
            else
                messageCount = value;
        }
    }

    public void Initialize()
    {
        current_rect = buttons["GameStatusBar_Button1"];
    }


    protected override void Awake()
    {
        GameManager.CombatManager.CombatAlert += BroadCastMessage;
        base.Awake();
        Initialize(); 
    }


    TMP_Text current; 
    TMP_Text previous;
    Button current_rect;
    Button previous_rect;
    string text_key;
    string button_key; 
    private void BroadCastMessage(string value)
    {
        text_key = $"Button{messageCount}_Message{messageCount}";
        button_key = $"GameStatusBar_Button{messageCount}"; 
        current = texts[text_key]; 
        current_rect = buttons[button_key];
        alerts.Enqueue(current);
        current_rect.transform.SetAsFirstSibling();
        upScale = StartCoroutine(RescaleButton(current_rect)); 
        if (alerts.Count == 1)
        {
            current.text = value;
            current.color = Color.red;
            return;
        }
        else
        {
            current.text = value;
            current.color = Color.red;
            previous_rect = transform.GetChild(1).GetComponent<Button>();
            previous = alerts.Dequeue();
            previous.color = Color.white;
            alerts.Enqueue(current);
            downScale = StartCoroutine(DownscaleButton(previous_rect)); 
        }
        MessageCount++; 
    }
    #region Replicating simply animation
    Coroutine upScale;
    Coroutine downScale;

    float timestep_up;
    float timestep_down;

    Vector3 currentScale;
    Vector3 upTargetScale;

    Vector3 otherScale;
    Vector3 downTargetScale;
    Vector3 defaultScale = Vector3.one;
    IEnumerator RescaleButton(Button button)
    {
        timestep_up = 0f;
        currentScale = button.transform.localScale;
        upTargetScale = button.transform.localScale * scaleSize;
        while (timestep_up < totalscaletime)
        {
            timestep_up += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(currentScale, upTargetScale, timestep_up / totalscaletime);
            yield return null;
        }
    }

    IEnumerator DownscaleButton(Button button)
    {
        timestep_down = 0f;
        otherScale = button.transform.localScale;
        downTargetScale = defaultScale;
        while (timestep_down < totalscaletime)
        {
            timestep_down += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(otherScale, Vector3.one, timestep_down / totalscaletime);
            yield return null;
        }
    }
    #endregion

}
