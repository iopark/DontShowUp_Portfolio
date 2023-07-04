using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBarUI : SceneUI
{
    //TODO: Should react to the weapon switch. 

    protected override void Awake()
    {
        GameManager.CombatManager.WeaponSwitch += SwitchActiveWeapon; 
        base.Awake();

    }

    public void Initialize()
    {
        texts["CurrentRound"].text = "2";
    }

    public void SwitchActiveWeapon()
    {

    }
}
