using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLostUI : PopUpUI
{
    Button untoMain_Button; 
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        untoMain_Button = buttons["Buttons_ExitToMain"];
        buttons["Buttons_ExitToMain"].onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        GameManager.SceneManager.LoadScene("TitleScene"); 
    }
}
