using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public UnityAction WeaponSwitch;

    public bool FlankJudgement()
    {
        return true; 
    }

    public bool IsHeadShot()
    {
        return true;
    }
    //Simply regards player's combat status. 
    //Flank Attack judgements, 
    //Account for any headshots.
    //Account for any playerget hit; 
}
