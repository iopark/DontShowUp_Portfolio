using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairUI : SceneUI
{
    //TODO: Should react to the weapon switch. 
    RectTransform primary;
    RectTransform secondary;
    RectTransform current;
    protected override void Awake()
    {

        base.Awake();
        Initialize();
    }
    private void OnEnable()
    {
        GameManager.CombatManager.WeaponSwitch += SwitchActiveWeapon;
    }
    private void OnDisable()
    {
        GameManager.CombatManager.WeaponSwitch -= SwitchActiveWeapon;
    }

    public void Initialize()
    {
        primary = transforms["CrossHair_Shotgun"];
        primary.gameObject.SetActive(true);
        secondary = transforms["CrossHair_Crossbow"];
        secondary.gameObject.SetActive(false);
        current = primary; 
    }


    public void SwitchActiveWeapon()
    {
        if (current.name == "Shotgun")
        {
            current.gameObject.SetActive(false);
            current = secondary;
            current.gameObject.SetActive(true);
        }
        else
        {
            current.gameObject.SetActive(false);
            current = primary;
            current.gameObject.SetActive(true);
        }
    }
}
