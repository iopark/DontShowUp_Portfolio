using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        // 
        buttons["ContinueButton"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); }); //��ǻ� �ڷΰ��� ��ư
        buttons["SettingsButton"].onClick.AddListener(() => { GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/ConfigPopUpUI"); }); // ������ �ְ��ϴ� UI �� �˾��ϰ� �մϴ�. 
        //ExitButton must close the game scene 

    }
}
