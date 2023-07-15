using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["Buttons_Sound"].onClick.AddListener(() => { ConfigureSound(); });
        buttons["Buttons_Continue"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
        buttons["Buttons_ExitToMain"].onClick.AddListener(() => { ExitToMenu(); });
    }

    void ConfigureSound()
    {
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/SoundPopUpUI");
    }

    void ExitToMenu()
    {
        GameManager.SceneManager.LoadScene("TitleScene");
    }
}
