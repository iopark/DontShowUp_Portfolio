using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        // 
        buttons["ContinueButton"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); }); //사실상 뒤로가기 버튼
        buttons["SettingsButton"].onClick.AddListener(() => { GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/ConfigPopUpUI"); }); // 설정을 주관하는 UI 를 팝업하게 합니다. 
        //ExitButton must close the game scene 

    }
}
