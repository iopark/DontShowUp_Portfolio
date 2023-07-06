using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["SaveButton"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
        buttons["CancelButton"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
    }
}
