using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndUI : PopUpUI
{
    string cleared = "Stage Cleared";
    string failed = "Stage Failed"; 
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        transforms["GameFinishedPopUpUI_Content"].gameObject.SetActive(false);
        transforms["GameFinishedPopUpUI_Buttons"].gameObject.SetActive(false); 
    }
    public void GameEnd(bool cleared)
    {
        if (cleared)
        {
            //Buttons to clear out
        }
    }
}
