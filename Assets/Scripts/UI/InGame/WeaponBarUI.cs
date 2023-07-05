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
    BarType currentType; 
    [SerializeField] float scaleSize = 1.3f;
    [SerializeField] float totalscaletime = 0.1f; 
    protected override void Awake()
    {
        
        base.Awake();
        Initialize();
    }
    private void OnEnable()
    {
        GameManager.CombatManager.WeaponSwitch += SwitchActiveWeapon;
        GameManager.CombatManager.WeaponFire += WeaponFireReact; 
    }
    private void OnDisable()
    {
        GameManager.CombatManager.WeaponSwitch -= SwitchActiveWeapon;
        GameManager.CombatManager.WeaponFire -= WeaponFireReact;
    }

    public void Initialize()
    {
        currentButton = buttons["WeaponBar_Primary"]; 
        texts["Primary_CurrentRound"].text = "2";
        texts["Primary_MaxRound"].text = "2";
        texts["Secondary_CurrentRound"].text = "1"; 
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

    Coroutine weaponUISwitch; 
    public void SwitchActiveWeapon()
    {
        StopAllCoroutines();
        currentButton.transform.localScale = transform.localScale;
        if (currentButton.name == "WeaponBar_Primary")
            currentButton = buttons["WeaponBar_Secondary"];
        else
            currentButton = buttons["WeaponBar_Primary"]; 
        StartCoroutine(RescaleButton(currentButton));
    }
    float timestep;
    Vector3 initialScale; 
    Vector3 targetScale; 
    IEnumerator RescaleButton(Button button)
    {
        timestep = 0f;
        initialScale = currentButton.transform.localScale; 
        targetScale = currentButton.transform.localScale * scaleSize;
        while (timestep < totalscaletime)
        {
            timestep += Time.deltaTime;
            currentButton.transform.localScale = Vector3.Lerp(initialScale, targetScale, timestep / totalscaletime);
            yield return null; 
        }
    }
}
