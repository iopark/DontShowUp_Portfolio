using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : Door
{
    string requireDiamonds = "Requires to Harvest More Diamonds"; 
    protected override void Awake()
    {
        base.Awake();
    }

    public bool ContestForGameEnd()
    {
        if (GameManager.DataManager.Diamond < GameManager.DataManager.TargetDiamonds)
        {
            GameManager.CombatManager.CombatAlert(requireDiamonds);
            return false;
        }
        else
            return true; 
    }
    public override bool ContestInteraction(float givenDist)
    {
        if (!ContestForGameEnd())
            return false; 
        return base.ContestInteraction(givenDist);
    }
}
