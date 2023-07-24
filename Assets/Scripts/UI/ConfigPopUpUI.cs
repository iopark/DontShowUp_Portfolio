using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["Buttons_Sound"].onClick.AddListener(() => { ConfigureSound(); });
        buttons["Buttons_Continue"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
        buttons["Buttons_Exit"].onClick.AddListener(() => { ExitToMenu(); });
    }

    private void OnPause(InputValue value)
    {

    }

    void ConfigureSound()
    {
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/"); 
    }

    void ExitToMenu()
    {
        GameManager.SceneManager.LoadScene("TitleScene");
    }
}
