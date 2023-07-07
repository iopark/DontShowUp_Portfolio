using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["SoundButton"].onClick.AddListener(() => { ConfigureSound(); });
        buttons["CancelButton"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
        buttons["ExitToMenu"].onClick.AddListener(() => { GameManager.UIManager.ClosePopUpUI(); });
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
