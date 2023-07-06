using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSceneUI : SceneUI
{
    // 게임의 세팅에 관여되는 PopUp UI 들을 주관하는 UI 들이며, 게임의 종료가 아닌이상 항시 옵션으로 보여지게 됩니다. 

    protected override void Awake()
    {
        base.Awake();

        buttons["InfoButton"].onClick.AddListener(() => { Debug.Log("Info"); });
        buttons["VolumeButton"].onClick.AddListener(() => { Debug.Log("Volume"); });
        buttons["SettingButton"].onClick.AddListener(() => { OpenPausePopUpUI(); });
    }

    public void OpenPausePopUpUI()
    {

        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI"); 
    }
    //public void ClickInfoButton()
    //{

    //}
}
