using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWonUI : PopUpUI
{
    Button untoMain_Button;
    [SerializeField] Sound victorySound;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        untoMain_Button = buttons["Buttons_ExitToMain"];
        untoMain_Button.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        GameManager.SceneManager.LoadScene("TitleScene");
        GameManager.UIManager.ClosePopUpUI(); 
    }
}
