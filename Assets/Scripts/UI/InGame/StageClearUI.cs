using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : PopUpUI
{
    Button untoNext_Button;
    Button untoMain_Button;
    string stageName; 

    protected override void Awake()
    {
        base.Awake();
        untoNext_Button = buttons["Buttons_Continue"];
        untoNext_Button.onClick.AddListener(ToNextStage); 
        untoMain_Button = buttons["Buttons_ExitToMain"];
        untoMain_Button.onClick.AddListener(ExitGame);
    }

    private void ToNextStage()
    {
        Time.timeScale = 1.0f;
        stageName = GameManager.DataManager.CurrentGameData.stageName;
        GameManager.SceneManager.LoadScene(stageName);
        GameManager.UIManager.ClosePopUpUI(); 
    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        GameManager.SceneManager.LoadScene("TitleScene");
    }
}
