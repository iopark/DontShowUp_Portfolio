using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    // All the UI which should be accessible through script, can be approached through its name within the dictionary. 

    protected Dictionary<string, RectTransform> transforms; 
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;

    protected virtual void Awake()
    {
        BindChildren(); 
    }

    // 매 프레임 별로 동작하는 활동에 대해서 
    // 캐싱하는 작업이 사실상 정배다. 

    private void BindChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
        //Base UI 를 기준으로 하위 자식들을 찾게 되었을때 

        RectTransform[] children = GetComponentsInChildren<RectTransform>(); 
        foreach (RectTransform child in children)
        {
            string key = child.gameObject.name; // key = gameobj's name
                                                // Order:
                                                // 1. Find All the components, 
                                                // 2. Track component's binded gameObj 
                                                // 3. use that gameObj's name and add them to the appropriate dictionary 
            if (transforms.ContainsKey(key))
                continue; 
            transforms.Add(key, child);

            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(key, button);
            }

            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
            {
                texts.Add(key, text);
            }
        }
    }
    public virtual void CloseUI()
    {

    }
}
