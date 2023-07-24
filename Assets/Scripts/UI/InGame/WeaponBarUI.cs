using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    WeaponBar_Primary, 
    WeaponBar_Secondary
}
public class WeaponBarUI : SceneUI
{
    //TODO: Should react to the weapon switch. 
    Button currentButton;
    Button otherButton; 
    BarType currentType; 
    [SerializeField] float scaleSize = 1.3f;
    [SerializeField] float totalscaletime = 0.1f; 
    protected override void Awake()
    {
        
        base.Awake();
        Initialize();
    }
    private void Start()
    {
        GameManager.CombatManager.WeaponSwitch += SwitchActiveWeapon;
        GameManager.CombatManager.WeaponFire += WeaponFireReact;
    }
    private void OnEnable()
    {
        Initialize();
    }
    private void OnDisable()
    {
        //GameManager.CombatManager.WeaponSwitch -= SwitchActiveWeapon;
        //GameManager.CombatManager.WeaponFire -= WeaponFireReact;
    }

    public void Initialize()
    {
        currentButton = buttons["WeaponBar_Primary"];
        otherButton = buttons["WeaponBar_Secondary"]; 
        texts["Primary_CurrentRound"].text = "2";
        texts["Primary_MaxRound"].text = "2";
        texts["Secondary_CurrentRound"].text = "0"; 
        texts["Secondary_MaxRound"].text = "1";
    }

    public void WeaponFireReact(int value)
    {
        switch(currentType)
        {
            case BarType.WeaponBar_Primary:
                texts["Primary_CurrentRound"].text = value.ToString(); break;
            case BarType.WeaponBar_Secondary:
                texts["Secondary_CurrentRound"].text = value.ToString(); break;
        }
    }

    Coroutine upScale;
    Coroutine downScale; 
    public void SwitchActiveWeapon()
    {
        StopAllCoroutines();
        currentButton.transform.localScale = transform.localScale;
        if (currentButton.name == "Primary")
        {
            currentButton = buttons["WeaponBar_Secondary"];
            currentType = BarType.WeaponBar_Secondary;
            otherButton = buttons["WeaponBar_Primary"]; 
        }
        else
        {
            currentButton = buttons["WeaponBar_Primary"];
            currentType = BarType.WeaponBar_Primary;
            otherButton = buttons["WeaponBar_Secondary"];
        }
        upScale = StartCoroutine(RescaleButton(currentButton));
        downScale = StartCoroutine(DownscaleButton(otherButton)); 
    }
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
}
