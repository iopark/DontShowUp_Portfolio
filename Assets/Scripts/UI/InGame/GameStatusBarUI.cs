using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusBarUI : SceneUI
{
    [SerializeField] float scaleSize = 1.3f;
    [SerializeField] float totalscaletime = 0.1f;

    RectTransform previous; 
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
    string key; 
    protected override void Awake()
    {
        GameManager.CombatManager.CombatAlert += BroadCastMessage;
        base.Awake();
    }

    private void BroadCastMessage(string value)
    {
        key = $"GameStatusBar_Message{messageCount}";
        texts[key].text = value;
        transforms[key].SetAsFirstSibling();
        MessageCount++; 
    }

    private void SetToTop()
    {

    }

    float timestep;
    Vector3 initialScale;
    Vector3 targetScale;
    IEnumerator RescaleAlert(RectTransform subject)
    {
        timestep = 0f;
        initialScale = subject.localScale;
        targetScale = subject.localScale * scaleSize;
        while (timestep < totalscaletime)
        {
            timestep += Time.deltaTime;
            subject.localScale = Vector3.Lerp(initialScale, targetScale, timestep / totalscaletime);
            yield return null;
        }
    }
}
