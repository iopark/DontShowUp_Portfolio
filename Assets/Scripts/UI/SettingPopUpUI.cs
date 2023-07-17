using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["Buttons_Sound"].onClick.AddListener(() => { ConfigureSound(); });
        buttons["Buttons_BackToGame"].onClick.AddListener(() => { ReturnToGame(); });
        buttons["Buttons_ExitToMain"].onClick.AddListener(() => { ExitToMenu(); });
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined; 
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }
    
    void ReturnToGame()
    {
        GameManager.DataManager.PauseGame?.Invoke(); 
        GameManager.UIManager.ClosePopUpUI();
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
