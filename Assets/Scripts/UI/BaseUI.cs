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
    protected Dictionary<string, Slider> sliders; 
    protected Dictionary<string, TMP_Text> texts;

    protected virtual void Awake()
    {
        BindChildren(); 
    }

    private void RecursiveBindingGrandChild(RectTransform child)
    {
        RectTransform[] children = child.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform grandchild in children)
        {
            string key = $"{child.gameObject.name}_{grandchild.gameObject.name}";
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
    private void BindChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        sliders = new Dictionary<string, Slider>();
        texts = new Dictionary<string, TMP_Text>();

        RectTransform[] children = GetComponentsInChildren<RectTransform>(true); 
        foreach (RectTransform child in children)
        {
            //if (child.childCount > 0)
            //{
            //    RecursiveBindingGrandChild(child);
            //}
            string key; 
            if (child.parent != null)
            {
                key = $"{child.parent.gameObject.name}_{child.gameObject.name}";
            }
            else 
                key = child.gameObject.name;
            if (transforms.ContainsKey(key))
                continue; 
            transforms.Add(key, child);

            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(key, button);
            }

            Slider slider = child.GetComponent <Slider>();
            if (slider != null)
            {
                sliders.Add(key, slider);
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
